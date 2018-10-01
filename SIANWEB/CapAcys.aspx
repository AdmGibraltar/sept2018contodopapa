<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="True" CodeBehind="CapAcys.aspx.cs" Inherits="SIANWEB.CapAcys"  EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>


<style>
[draggable=true] {
    cursor: move;
}
    
    
    table.tblCalendario td
    {
        width:130px;
        height:300px;
       border-style:solid;
     border-width   : 1px;
     vertical-align:text-top;
     overflow:scroll;
    }
    
    .divItemsCalendario_rgAcuerdos, .divItemsCalendario_rgAcuerdos_Kilo, .divItemsCalendario_rgAcuerdos_Comensal, .divItemsCalendario_rgAcuerdos_Habitacion, .divItemsCalendario_rgAcuerdos_Iguala
    { 
        height:300px;
        display:none;
    }
    
</style>
    <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
               <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnAutorizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ContactoRepVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          <%--  <telerik:AjaxSetting AjaxControlID="cmbRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
             <telerik:AjaxSetting AjaxControlID="ChkServAsesoria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="ChkServTecnicoRelleno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkServMantenimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="rdVigenciaIni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdFechaInicioDocumento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdFechaFinDocumento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcuerdos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal2" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal3" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal4" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal5" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal6" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>


             <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Kilo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Kilo" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal2" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal3" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal4" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal5" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal6" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>


             <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Comensal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Comensal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal2" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal3" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal4" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal5" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal6" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Habitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Habitacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal2" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal3" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal4" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal5" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal6" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Iguala">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Iguala" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal2" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal3" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal4" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal5" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="tCal6" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAsesoria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsesoria" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgServicios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgServicios" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMantPrevRev">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMantPrevRev" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rdModFrencuenciaEstablecida">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <%--<telerik:AjaxSetting AjaxControlID="rdModOrdenAbierta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>        
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick"
        onclientbuttonclicked="ToolBar_ClientClick">
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
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
            
             <telerik:RadToolBarButton CommandName="Bitacora" Value="Bitacora" ToolTip="Mostrar bitácora de cambios" CssClass="save"
                    ImageUrl="~/Imagenes/informacion25.png" />         
        </Items>
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" style="font-family: verdana; font-size: 9pt" runat="server">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label102" runat="server" Text="Folio" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtFolio" runat="server" enabled="False" maxlength="9"
                                    minvalue="1" width="70px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" /> 
                                <ClientEvents OnKeyPress="handleClickEvent" /> 
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Versión" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtVersion" runat="server" enabled="False" maxlength="9"
                                    minvalue="1" width="70px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" /> 
                                <ClientEvents OnKeyPress="handleClickEvent" /> 
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>


                            <td width="100px" style="text-align: right;">
                                Fecha
                            </td>
                            <td>
                                <telerik:RadDatePicker id="rdFecha" runat="server" width="90px" culture="es-MX">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                    <ClientEvents OnDateClick="Calendar_Click" />
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" >
                                     <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdFecha"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Inicio
                            </td>
                            <td>                               
                                <telerik:RadDatePicker ID="rdFechaInicioDocumento" runat="server" Width="100px" OnSelectedDateChanged="rdFechaInicioDocumento_SelectedDateChanged"
                                AutoPostBack="True" Culture="es-MX" enabled = "False">
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rdFechaInicioDocumento"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Fin
                            </td>
                            <td>
                             <telerik:RadDatePicker ID="rdFechaFinDocumento" runat="server" Width="100px" OnSelectedDateChanged="rdFechaFinDocumento_SelectedDateChanged"
                                AutoPostBack="True" Culture="es-MX">
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"  >
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal"  />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rdFechaFinDocumento"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>                           
                            </td>
                            <td width="250">
                            </td>
                            <td width="100px" style="text-align: right; "  visible="false">
                                Estatus
                            </td>
                             <td>
                                <telerik:RadTextBox id="txtEstatus" runat="server" enabled="False" width="70px" visible="false">
                                
                                
                                
                                </telerik:RadTextBox>

                                
                                                        
                            </td>
                            <td>
                                <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" 
                                ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" 
                                ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>                   

                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" >
                        <Tabs>
                            <telerik:RadTab runat="server" AccessKey="C" PageViewID="RadPageCliente" Text="1.-&lt;u&gt;C&lt;/u&gt;liente" ClientIDMode="Inherit"
                                 Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="R" PageViewID="RPVRecepcionPedido" Text="2.-&lt;u&gt;R&lt;/u&gt;ecepción de Pedidos">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="A" PageViewID="RPVAcuerdosEconomicos" Text="3.-&lt;u&gt;A&lt;/u&gt;cuerdo Económico de Producto">
                            </telerik:RadTab>
                             <telerik:RadTab runat="server" AccessKey="X" PageViewID="RPCalendario" Text="4.-<u>C</u>alendario" >
                            </telerik:RadTab>  
                            <telerik:RadTab runat="server" AccessKey="P" PageViewID="RPVCondicionesPago" Text="5.-Condiciones de &lt;u&gt;p&lt;/u&gt;ago">
                            </telerik:RadTab>                                                     
                            <telerik:RadTab runat="server" AccessKey="S" PageViewID="RPVServicio" Text="6.-&lt;u&gt;S&lt;/u&gt;ervicios de Valor">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="O" PageViewID="RPVOtrosApoyos" Text="7.-<u>O</u>tros Apoyos" >
                            </telerik:RadTab>         
                        </Tabs>
                    </telerik:RadTabStrip>
                    <div id="div1" runat="server">
                        <telerik:radmultipage id="RadMultiPage1" runat="server" borderstyle="Solid" borderwidth="1px"
                            scrollbars="Auto" selectedindex="0">
                            <%-- Height="415px" Width="880px">--%>
                            <telerik:RadPageView ID="RadPageCliente" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="99%" Height="415px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="99%" Height="410px" OnClientResized="onResize"
                                        BorderColor="Red">
                                        <div runat="server" id="divGenerales" style="font-family: verdana; font-size: 8pt">                                                                                                                                                                                                                              
                                            <table style="border:1px solid black;"  >                                                                                                                                 
                                              <thead >
                                                <tr>
                                                <th  style="font-family:  verdana; font-size: 10pt; border:1px solid black;  border-collapse:collapse;" colspan="21"    >  CLIENTE</th>                                                         
                                                </tr>
                                                <tr>
                                                <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  Información Fiscal</th>                                                         
                                                </tr>
                                              </thead> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                           
                                                <tr> 
                                                    <td>
                                                    </td>
                                                    <td> 
                                                        <asp:Label ID="LabelCliente" runat="server" Text="Cliente"/>
                                                    </td>
                                                    <td width="70">
                                                        <telerik:RadNumericTextBox ID="txtCliente" runat="server" AutoPostBack="True" MaxLength="9"
                                                            MinValue="1" OnTextChanged="txtCliente_TextChanged" Width="70px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator=""/>
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>    
                                                    </td>                                                                                                          
                                                    <td colspan="10">
                                                        <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="True" Width="428px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td Width="20px" >
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCliente"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar">
                                                            </asp:RequiredFieldValidator>
                                                        <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                            ToolTip="Buscar" ValidationGroup="buscar" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Sucursal"/>
                                                    </td>                                                     
                                                     <td>
                                                        <telerik:RadTextBox ID="txtSucursal" runat="server" ReadOnly="False" Width="128px">
                                                        </telerik:RadTextBox>                                                     
                                                     </td>                                                                                                         
                                                     <td width="50"><asp:Label ID="Label811111" runat="server" Text="Segmento"/></td>
                                                     <td  width="50">
                                                        <telerik:RadTextBox ID="txtSegmento" runat="server" ReadOnly="True" Width="128px">
                                                        </telerik:RadTextBox>  
                                                     </td>
                                                     <td> </td>
                                                     <td></td>
                                                    <td></td>
                                                </tr>
                                                 <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label59" runat="server" Text="Dirección"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txtClienteDireccion" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label60" runat="server" Text="Colonia"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtClienteColonia" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                  
                                                   <td colspan="1">  <asp:Label ID="Label61" runat="server" Text="Municipio"></asp:Label>         </td> 
                                                      <td colspan="6">
                                                        <telerik:RadTextBox ID="txtClienteMunicipio" runat="server" ReadOnly="False" width="410">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                     
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label62" runat="server" Text="Estado"/></td>                                                  
                                                    <td colspan="2">
                                                        <telerik:RadTextBox ID="txtClienteEstado" runat="server" ReadOnly="False" width="185" >
                                                        </telerik:RadTextBox>                       </td>                                                                                                                                                              
                                                    <td> <asp:Label ID="Label63" runat="server" Text="C.P." style="text-align:center;"  width="30" ></asp:Label> </td> 
                                                    <td colspan="1">
                                                        <telerik:RadTextBox ID="txtClienteCodPost" runat="server" ReadOnly="False" width="47">
                                                        </telerik:RadTextBox> </td>
                                                    <td colspan="1">  <asp:Label ID="Label64" runat="server" Text="RFC" width="40" style="text-align:center;" ></asp:Label> </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtClienteRFC" runat="server" ReadOnly="False" width="175" >
                                                        </telerik:RadTextBox> </td> 
                                                    <td></td>   
                                                    <td> 
                                                        <asp:Label ID="Label65" runat="server" Text="Addenda"></asp:Label>
                                                    </td>
                                                    <td> <asp:CheckBox ID="ChkbAdendaSI" runat="server" Text="SI" ReadOnly="True" /> </td>                                                    
                                                    <td> </td>                                                        
                                                    <td colspan="4"><asp:Label ID="Label145" runat="server" Text="Asignación de pedido"></asp:Label>
                                                    <telerik:RadComboBox ID="cmbAsignacion" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="200px"
                                                        LoadingMessage="Cargando...">
                                                    </telerik:RadComboBox></td>   

                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="Label106" runat="server" Text="E-mail de recibo de factura electrónica"></asp:Label>
                                                    </td>
                                                    <td colspan="3" >
                                                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="231px" MaxLength="50" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" />
                                                        </telerik:RadTextBox>
                                                    
                                                    </td>
                                                     <td colspan="9">&#160; </td>                                                      
                                                     <td> <asp:Label ID="Label9" runat="server" Text="Cuenta Corporativa"></asp:Label> </td>
                                                    <td> <asp:CheckBox ID="CheckCuentaCorporativa" runat="server" Text="SI" ReadOnly="True"/> </td>  
                                                </tr>


                                                <tr>
                                                    <td></td>
                                                    <td colspan="14">
						                                <asp:Label ID="lblTipoCliente" runat="server" Text="Tipo de Cliente"></asp:Label>

                                                        <telerik:RadNumericTextBox ID="txtIdTipoCliente" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents  OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>      
                                                    <telerik:RadComboBox ID="cmbTipoCliente" runat="server" DataTextField="Descripcion"
                                                        DataValueField="Id" EnableLoadOnDemand="True" Filter="Contains" HighlightTemplatedItems="True"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="True" 
                                                        Width="255px"
                                                        AutoPostBack="True">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                                        </Items>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center" valign="middle">
                                                                        <asp:Label ID="lblTipoClienteId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left" valign="middle">
                                                                        <asp:Label ID="LblcmbTipoClienteDescripcion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>                                                                                                      
                                                    </td>
                                                </tr>


                                                  <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                                        
                                                <tr>
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  Información Comercial</th>                                                         
                                                </tr> 
                                                 <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>                                                    
                                                <tr > 
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label26" runat="server" Text="Nombre comercial" /> </td>
                                                    <td colspan="4" style="margin-left: 40px">
                                                        <telerik:RadTextBox ID="txtComercial" runat="server" Width="275px" MaxLength="250" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                
                                                     <td colspan="7">
                                                        <asp:Label ID="Label67" runat="server" Text="Dirección de Entrega Producto"></asp:Label>
                                                    </td>
                                                    <td>                                                    
                                                            <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarDireccionEntrega_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCliente"
                                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                            </asp:RequiredFieldValidator>
                                                     </td>                                                   
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtDireccionEntrega" runat="server" ReadOnly="True" Width="400px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td> </td>                                                                                                          
                                                </tr>
                                                  <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label68" runat="server" Text="Colonia"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txtClienteColoniaE" runat="server" ReadOnly="True" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label69" runat="server" Text="Municipio"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtClienteMunicipioE" runat="server"  width="220">
                                                        </telerik:RadTextBox>     
                                                       </td>     
                                                        <td></td>
                                                     <td colspan="1">
                                                       <asp:Label ID="Label70" runat="server" Text="Estado" ReadOnly="True"></asp:Label> 
                                                    </td>                                                                                                                                                     
                                                      <td colspan="2">
                                                        <telerik:RadTextBox ID="txtClienteEstadoE" runat="server" Enabled="False" width="122">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                     <td colspan="1">  <asp:Label ID="Label71" runat="server" Text="C.P."></asp:Label>   </td>   
                                                     <td colspan="1"  >
                                                        <telerik:RadTextBox ID="txtClienteCPE" runat="server" ReadOnly="True" width="60">
                                                        </telerik:RadTextBox>
                                                    </td>  
                                                    <td colspan="1"> <asp:Label ID="Label25" runat="server" Text="No proveedor" /> </td>
                                                    <td colspan="2"> <telerik:RadTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                                                                                                                    
                                                </tr>
                                                <tr> 
                                                    <td> </td>
                                                     <td>  <asp:Label ID="Label3" runat="server" Text="Contacto principal" /> </td>
                                                    <td  colspan="4">
                                                        <telerik:RadTextBox ID="txtContacto" runat="server" Width="275px" MaxLength="100" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>                                                    
                                                    <td>   <asp:Label ID="Label4" runat="server" Text="Puesto" />  </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtPuesto" runat="server" Width="175px" MaxLength="50" ReadOnly="True">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        
                                                    </td>                                                    
                                                     <td >
                                                        <asp:Label ID="Label6" runat="server" Text="Teléfono" />
                                                    </td>
                                                    <td colspan="2" >
                                                        <telerik:RadTextBox ID="txtTelefono" runat="server"  Width="122px"  MaxLength="20" ReadOnly="True">                                                         
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />                                                            
                                                        </telerik:RadTextBox>
                                                    </td>
                                                      <td >
                                                        
                                                    </td>
                                                    <td colspan="3" >                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td><asp:Label ID="LabelTerritorio" runat="server" Text="Territorio" />    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                            Width="70px" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                     
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            MaxHeight="250px" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbTer_SelectedIndexChanged" Width="370px" ReadOnly="True">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td> 
                                                     <td Width="20px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTerritorio"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"  ></asp:RequiredFieldValidator>
                                                    </td>   
                                                     <td  Width="20px" ></td>                                                   
                                                    <td  >   </td> 
                                                    <td colspan="1" >   
                                                        <asp:Label ID="Label1" runat="server" Text="Ruta de servicio"></asp:Label> 
                                                    </td>
                                                    <td  colspan="1">
                                                        <telerik:RadNumericTextBox ID="txtRutaServicio" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt4_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                     <td  colspan="6">
                                                        <telerik:RadComboBox ID="cmbRutaServicio" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb4_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="True">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="LabelRepresentante" runat="server" Text="Representante de Ventas" />   </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" MinValue="1" Width="70px"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbRepresentante" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione un territorio"
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbRepresentante_SelectedIndexChanged" Width="370px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>                                                                                                       
                                                    <td Width="20px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRepresentante"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                    </td>  
                                                    <td></td>    
                                                    <td></td> 
                                                    <td><asp:Label ID="Label2" runat="server" Text="Ruta de entrega"></asp:Label> </td>
                                                    <td colspan="1">  <telerik:RadNumericTextBox ID="txtRutaEntrega" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="True">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt5_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>                  </td>
                                                    <td colspan="6">
                                                        <telerik:RadComboBox ID="cmbRutaEntrega" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true" 
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb5_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="True">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>                                                                                                                                                            
                                                </tr>





                                                <tr>
                                                    <td> </td>
                                                    <td colspan="13"> 
<asp:Label ID="Label207" runat="server" Text="Garantia"/><asp:CheckBox ID="ChkbGarantiaSI" runat="server" Text="Si"  />
<asp:Label ID="Label208" runat="server" Text="Servicios"/><asp:CheckBox ID="ChkbServiciosSI" runat="server" Text="Si" />                                                    
                                                    </td>
                                                </tr>

                                                 <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr>  
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RPVRecepcionPedido" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane2" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="div2" style="font-family: verdana; font-size: 8pt">                                    
                                        <table border="0" width="100%" >                                                                                                                                 
                                              <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 2.- MODALIDAD DE PEDIDO:</th>                                                         
                                                </tr>                                                        
                                              </thead>  
                                                <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModFrencuenciaEstablecida" runat="server" Text="Frecuencia Establecida"  GroupName="ModalidadPedido" OnCheckedChanged="RbModalidad_CheckedChanged" AutoPostBack="True" Checked="true" />
                                                </td>
                                                <td colspan="2">
                                                 <%--
                                                 <asp:RadioButton ID="rdModOrdenAbierta" runat="server" Text="Orden Abierta con Reposición / Release"  GroupName="ModalidadPedido" OnCheckedChanged="RbModalidad_CheckedChanged" AutoPostBack="True"/>
                                                 --%>&nbsp;
                                                </td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModConsignacion"  runat="server" Text="" GroupName="ModalidadPedido" Enabled="false" />
                                                 </td><td colspan="2">
                                                 <asp:Label ID="Label209" runat="server" Text="Cliente acepta Parcialidades"/><asp:CheckBox ID="CheckParcialidadesSi" runat="server" Text="Si"  /><asp:CheckBox ID="CheckParcialidadesNo" runat="server" Text="No" />                                                    
                                                </td>
                                              </tr> 
                                               
                                               <tr>
                                                <td></td>
                                              </tr> 
                                              <tr>
                                                <td></td>
                                              </tr> 
                                               <tr>
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  2.1 EL CLIENTE ENVIARÁ LOS PEDIDOS VÍA:</th>                                                         
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>                                             
                                                <tr> 
                                                    <td></td>                                                   
                                                    <td> <asp:CheckBox ID="ChkbEmail" runat="server" Text="Email" />  </td>
                                                     <td width="100px"> </td>
                                                    <td ><asp:CheckBox ID="ChkbFax" runat="server" Text="Fax"/> </td>
                                                    <td><asp:CheckBox ID="ChkbTelefono" runat="server" Text="Teléfono"/> </td>    
                                                    <td colspan="2">&#160;</td>                                                
                                                    <td colspan="3"><asp:CheckBox ID="CheckRepVenta" runat="server" Text="Recolectado por el Rep. de Ventas"/> </td>
                                                    
                                                </tr>
                                                 <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label97" runat="server" Text="Otro:"/></td>
                                                    <td colspan="7"  >
                                                        <telerik:RadTextBox ID="txtPedidoOtro" runat="server" ReadOnly="False" width="600">
                                                        </telerik:RadTextBox> 
                                                     </td>                                                                                                                                                                                                                                                                                                                                    
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td  colspan="1"> <asp:Label ID="Label100" runat="server" Text="Nombre de la Persona Encargada de Enviar el Pedido:"/></td>                                                  
                                                    <td colspan="5">
                                                        <telerik:RadTextBox ID="txtPedidoEncargadoEnviar" runat="server" ReadOnly="False" width="600" >
                                                        </telerik:RadTextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtPedidoEncargadoEnviar"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>--%>
                                                     </td>                                                                                                                                                                                                                  
                                                </tr>                                                                                                                                                                                                                                                     
                                                <tr> 
                                                    <td></td>
                                                    <td> <asp:Label ID="LabelPuesto" runat="server" Text="Puesto:" /> </td>
                                                    <td colspan="2" >
                                                        <telerik:RadTextBox ID="txtpedidoPuesto" runat="server" Width="245px" MaxLength="250">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtpedidoPuesto"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> --%>
                                                     </td>                                                                                                                                                
                                                     <td colspan="1">
                                                        <asp:Label ID="LabelPedidoTelefono" runat="server" Text="Teléfono:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="2">
                                                    <telerik:RadTextBox ID="txtpedidotelefono" runat="server" Width="180px">
                                                        </telerik:RadTextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtpedidotelefono"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>--%>
                                                    </td>  
                                                     <td colspan="1"  width="50px">
                                                        <asp:Label ID="Label96" runat="server" Text="Email:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="1" Width="264px">
                                                        <telerik:RadTextBox ID="txtpedidoEmail" runat="server"  Width="257px">
                                                        </telerik:RadTextBox>
                                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtpedidoEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpedidoEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                    </td>                                                   
                                                </tr> 
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">                                                        
                                                    </td>
                                                    <td colspan="4">                                                     
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr> 
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr> 
                                                <tr>
                                                    <td>
                                                    </td>
                                                  <td><%--Documentación requerida para entrega--%>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <%--<asp:CheckBox ID="chkRecDocOrdenCompra" runat="server" Text="Orden de compra / Release" />--%>&nbsp;
                                                    </td>                                                    
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label55" runat="server" Text="Otro:"/></td>
                                                    <td colspan="6"  >
                                                        <telerik:RadTextBox ID="txtRecDocOtro" runat="server" ReadOnly="False" width="600">
                                                        </telerik:RadTextBox> 
                                                     </td>         
                                                    <td colspan="1"  >                                                     
                                                 
                                                 <asp:Label ID="Label210" runat="server" Text="Cliente requiere Confirmación de Pedidos"/><asp:CheckBox ID="CheckConfirmacionPedidosSI" runat="server" Text="Si"  /><asp:CheckBox ID="CheckConfirmacionPedidosNO" runat="server" Text="No" />                                                    
                                                     </td>                                                                                                                                                                                                                                                                                                                           
                                                </tr>
                                                 
                                                 
                                              <tr> 
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21" align="center" >
                                                     <asp:HiddenField ID="HF_ID" runat="server" />   
                                                        <asp:HiddenField ID="HF_Sustituye" runat="server" />   
                                                         <asp:HiddenField ID="IdCte_DirEntrega" runat="server" />2.2 DOCUMENTOS PARA ENTREGAR Y RECEPCION</th>
                                              </tr>                                                 
                                                                                                                                              
                                            </table>                                                                                 


                                               <%--
                                                    
                                               
                                            <table>            
                                                                                       
                                                                             
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:HiddenField ID="HF_ID" runat="server" />   
                                                        <asp:HiddenField ID="HF_Sustituye" runat="server" />   
                                                         <asp:HiddenField ID="IdCte_DirEntrega" runat="server" />   
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                              <tr> 
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="8" align="center" >2.2 DOCUMENTOS PARA ENTREGAR Y RECEPCION</th>
                                              </tr>
                                            </table>--%>
<table width="100%" border="0">
                                        <tr>
                                         </tr>  
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td height="10" width="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label7111" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label82222" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label7111111" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label8" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label99111" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label99" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label104a" runat="server" Text="Días de recepción"></asp:Label>
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevLunes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevMartes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevMiercoles" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevJueves" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevViernes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRecRevSabado" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>                                                        
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="120">
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
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label105" runat="server" Text="Horarios de recepción"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Culture="es-MX"
                                                                    Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar14" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView9" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput14" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar15" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView10" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput15" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker3" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar16" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView11" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput16" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker4" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar17" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView12" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput17" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label104" runat="server" Text="Persona que recibe: "></asp:Label>
                                                              </td>   
                                                              <td  colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPersonaRecibe" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                              <td valign="top" width="100">
                                                                <asp:Label ID="Label110" runat="server" Text="Puesto: "></asp:Label>
                                                              </td>   
                                                              <td colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPuesto" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                                                              
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label111" runat="server" Text="Cita para entrega: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chkRecCitaMismoDia" runat="server" Text="Mismo día" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chkRecCitaSinCita" runat="server" Text="Sin cita" /></td>    
                                                              <td colspan="2"><asp:CheckBox ID="chkRecCitaPrevia" runat="server" Text="Previa" /></td>  
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Contacto:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txtRecCitaContacto" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td colspan="6"></td>
                                                            <td>Teléfono:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txtRecCitaTelefono" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Dias de Anticipación:</td>
                                                            <td>     

                                                                        <telerik:RadNumericTextBox ID="txtRecCitaDiasdeAnticipacion" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label112" runat="server" Text="Área de recibo: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chkRecAreaPropia" runat="server" Text="Propia" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chkRecAreaPlaza" runat="server" Text="Plaza" /></td>    
                                                              <td colspan="1"><asp:CheckBox ID="chkRecAreaCalle" runat="server" Text="Calle" /></td>  
                                                              <td ><asp:CheckBox ID="chkRecAreaAvTransitada" runat="server" Text="Avenida transitada" /></td>  
                                                        </tr>
                                                       <tr>
                                                              <td colspan="2"><asp:Label ID="Label113" runat="server" Text="Estacionamiento: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chkRecEstCortesia" runat="server" Text="Cortesía" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chkRecEstCosto" runat="server" Text="Costo" /></td> 
                                                              <td colspan="1"><asp:Label ID="Label114" runat="server" Text="Monto: " /></td>   
                                                              <td >
                                                                    <telerik:RadNumericTextBox ID="txtRecEstMonto" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                              </td>  

                                                        </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                       <table>
                                                        <tr>
                                                            <td><asp:Label ID="Label1132" runat="server" Text="Documentos" /></td>
                                                            <td><asp:Label ID="Label1142" runat="server" Text="Entrega" /></td>
                                                            <td><asp:Label ID="Label115" runat="server" Text="No. Copias" /></td>
                                                            <td><asp:Label ID="Label116" runat="server" Text="Recepción" /></td>
                                                            <td><asp:Label ID="Label117" runat="server" Text="No.Copias" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label118" runat="server" Text="Factura Franquicia" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactFranquiciaEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactFranquiciaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label119" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactKeyEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactKeyRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label120" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdCompraEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdCompraRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label121" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdReposEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdReposRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label122" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocCopPedidoEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocCopPedidoRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label123" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocRemisionEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocRemisionRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label124" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFolioEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFolioRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label125" runat="server" Text="Contra recibo" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocContraRecEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocContraRecRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label127" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocEntAlmacenEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocEntAlmacenRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label128" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocSopServicioEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocSopServicioRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label129" runat="server" Text="Nombre y Firma de recibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocNomFirmaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocNomFirmaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label126" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkRecCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecCitaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecCitaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
    
                                                </tr>
                                            </table>

                                                <br />

                                                <asp:Label ID="Label130" runat="server" Text="Otro" />: &nbsp;
                                                <telerik:RadTextBox ID="txtRecOtro" runat="server" ReadOnly="False" width="300">
                                                </telerik:RadTextBox>

                                                </td>
                                            </tr>
                                          </table>
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RPVAcuerdosEconomicos" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter3" runat="server" Width="100.5%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane3" runat="server" Width="100%" OnClientResized="onResize">                                     
                                        <div runat="server" id="divAcuerdosE" style="font-family: verdana; font-size: 8pt">
                                            <table >
                                                <tr>
                                                    <td width="10">
                                                    </td>
                                                    <td width="130">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td width="10">
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td width="150">
                                                    <asp:Label ID="lAnexo" runat="server" Text="ANEXO A" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="20">
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
                                                        
                                                    </td>
                                                     <a<td colspan="3">
                                                       &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text="Vigencia a partir de"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <telerik:RadDatePicker ID="rdVigenciaIni" runat="server" Width="100px" OnSelectedDateChanged="RadDatePicker2_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX">
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td colspan="3">
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rdVigenciaIni"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td style="width:100px">
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
                                                        <asp:Label ID="Label29" runat="server" Text="Semana inicial"></asp:Label>
                                                    </td>
                                                    <td colspan="5">
                                                        <telerik:RadNumericTextBox ID="txtSemana" runat="server" MinValue="1" Width="70px"
                                                            Enabled="False" MaxLength="9">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
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
                                                    <td colspan="3">
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUltFechaCorte" runat="server" Text="Ultima fecha de corte" Visible=false></asp:Label> 
                                                    </td>
                                                    <td>

                                                        <telerik:RadDatePicker id="rdFechaUltCorte" runat="server" width="90px" culture="es-MX" Visible=false>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>

                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="7">
                                                       
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                            &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="6">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                           &#160;
                                                    </td>
                                                    <td>
                                                            &#160;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="6">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td>


                                                 <div id="div_Regular" runat="server">


                                            <table width="100%">
                                                <tr>
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >3.1 ACUERDO ECONÓMICO DE PRODUCTO</th>                                                         
                                                </tr>
                                            </table>
                                            <%--       <table>
                                                    <tr>
                                                        <td style="width:200px"><b>FACTURADO</b></td> <td style="width:100px">&nbsp;</td>
                                                    </tr>
                                                     </table>--%>

                                                    <telerik:RadGrid ID="rgAcuerdos" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_ItemDataBound" OnItemCreated="rgAcuerdos_ItemCreated"
                                                        BorderStyle="None" ShowFooter="true" OnPreRender="rgAcuerdos_PreRender">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'
	                                                                    MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
																				Width="100px">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
	                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto" 
                                                           UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>' Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Prd_Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Acys_Cantidad")),Convert.ToDouble(Eval("Prd_Precio"))).ToString("N2")%>'></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaInicio" HeaderText="Fecha Inicio" UniqueName="Acs_ConsigFechaInicio"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acys_ConsigFechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpConsigFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaInicio") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaFin" HeaderText="Fecha Fin" UniqueName="Acs_ConsigFechaFin"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("Acys_ConsigFechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                      
                                                                         <telerik:RadDatePicker ID="tpConsigFechaFin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaFin") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                               
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("Acys_CantTotal" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" DbValue='<%# Bind("Acys_CantTotal") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
													</telerik:RadGrid>
                                                </div>


                                            <table width="100%">
                                                <tr>
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >3.2 ACUERDO ECONÓMICO DE SERVICIOS</th>                                                         
                                                </tr>
                                            </table>


                                            <table width="100%">
                                                <tr>
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >3.3 ACUERDO ECONÓMICO DE GARANTIAS</th>
                                                </tr>
                                            </table>





                                            <div id="div_Kilo" runat="server">
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td style="width:200px"><b>GARANTIA POR KILO DE LAVADO</b></td> <td style="width:100px">&nbsp;</td>

                                                        <td>Factor Garantía</td>
                                                        <td>
                                                <%--        <asp:TextBox ID="Fac_Kilo" runat="Server"></asp:TextBox>--%>

                                                            
                                                            <telerik:RadNumericTextBox ID="Fac_Kilo" runat="server" 
                                                                            MinValue="0" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                             </telerik:RadNumericTextBox>

                                                           
                                                            <asp:RequiredFieldValidator runat="server" ID="valFac_Kilo" ControlToValidate="Fac_Kilo"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
                                                        
                                                        </td><td></td>

                                                        <td>U. PRIMA META</td><td>
                                             

                                                          <telerik:RadNumericTextBox ID="PNeta_Kilo" runat="server" 
                                                                            MinValue="0"  MaxValue="100" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                             </telerik:RadNumericTextBox>

                                                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="PNeta_Kilo"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
                                                        
                                                        </td><td></td>

                                                        <td>Fecha Corte</td>
                                                        <td>     
                                                            <asp:ImageButton ID="imgKilo" runat="server" ImageUrl="~/Img/find16.png" 
                                                            OnClick="imgKilo_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                                                                                            
                                                         </td>
                                                        


                                                        <td></td>
                                                    </tr>
                                                </table>

                                                   <%-- Grid Kilo--%>
                                                  <telerik:RadGrid ID="rgAcuerdos_Kilo" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_Kilo_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_Kilo_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_Kilo_ItemDataBound" OnItemCreated="rgAcuerdos_Kilo_ItemCreated"
                                                        BorderStyle="None" ShowFooter="true" OnPreRender="rgAcuerdos_Kilo_PreRender">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd") %>'
                                                                            MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto"
                                                                    UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox Enabled="False" ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Prd_Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Acys_Cantidad")),Convert.ToDouble(Eval("Prd_Precio"))).ToString("N2")%>'></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                               
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaInicio" HeaderText="Fecha Inicio" UniqueName="Acs_ConsigFechaInicio"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acys_ConsigFechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpConsigFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaInicio") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaFin" HeaderText="Fecha Fin" UniqueName="Acs_ConsigFechaFin"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("Acys_ConsigFechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                      
                                                                         <telerik:RadDatePicker ID="tpConsigFechaFin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaFin") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                               
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("Acys_CantTotal" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" DbValue='<%# Bind("Acys_CantTotal") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                </div>
                                                  <%--  Grid Comensal--%>

                                         <div id="div_Comensal" runat="server">
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td style="width:200px"><b>GARANTIA POR COMENSAL</b></td> <td style="width:100px">&nbsp;</td>

                                                        <td>Factor Garantía</td><td>
                                                                                                           
                                                         <telerik:RadNumericTextBox ID="Fac_Comensal" runat="server" 
                                                                            MinValue="0" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                         </telerik:RadNumericTextBox>
                                                        
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="Fac_Comensal"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />   
                                                        
                                                        </td><td></td>

                                                        <td>U. PRIMA META</td><td>

                                                          <telerik:RadNumericTextBox ID="PNeta_Comensal" runat="server" 
                                                                            MinValue="0" MaxValue="100" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                             </telerik:RadNumericTextBox>

                                                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="PNeta_Comensal"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />   
                                                        
                                                        </td><td></td>

                                                        <td>Fecha Corte</td><td>
                                                        
                                                        <asp:ImageButton ID="imgComensal" runat="server" ImageUrl="~/Img/find16.png" 
                                                            OnClick="imgComensal_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />

                                                        </td><td></td>
                                                    </tr>
                                                </table>

                                            <telerik:RadGrid ID="rgAcuerdos_Comensal" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_Comensal_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_Comensal_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_Comensal_ItemDataBound" OnItemCreated="rgAcuerdos_Comensal_ItemCreated"
                                                        BorderStyle="None" ShowFooter="true" OnPreRender="rgAcuerdos_Comensal_PreRender">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd") %>'
                                                                            MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto"
                                                                    UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox  Enabled="False" ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Prd_Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Acys_Cantidad")),Convert.ToDouble(Eval("Prd_Precio"))).ToString("N2")%>'></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                               
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaInicio" HeaderText="Fecha Inicio" UniqueName="Acs_ConsigFechaInicio"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acys_ConsigFechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpConsigFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaInicio") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaFin" HeaderText="Fecha Fin" UniqueName="Acs_ConsigFechaFin"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("Acys_ConsigFechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                      
                                                                         <telerik:RadDatePicker ID="tpConsigFechaFin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaFin") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                               
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("Acys_CantTotal" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" DbValue='<%# Bind("Acys_CantTotal") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>


 
                                                </div>

                                                 <%--   Grid habitación--%>
                                                    
                                            <div id="div_Habitacion" runat="server">
                                              <br />
                                                <table>
                                                    <tr>
                                                        <td style="width:200px"><b>GARANTIA POR HABITACION</b></td> <td style="width:100px">&nbsp;</td>

                                                        <td>Factor Garantía</td><td>
                                                        
                                                        <telerik:RadNumericTextBox ID="Fac_Habitacion" runat="server" 
                                                                            MinValue="0" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>

                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="Fac_Habitacion"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />   
                                                        
                                                        </td>
                                                        
                                                        <td></td>

                                                        <td>U. PRIMA META</td><td>
                                                        
                                                         <telerik:RadNumericTextBox ID="PNeta_Habitacion" runat="server" 
                                                                            MinValue="0"  MaxValue="100" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                         </telerik:RadNumericTextBox>

                                                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="PNeta_Habitacion"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />   

                                                        </td><td></td>

                                                        <td>Fecha Corte</td>
                                                        <td>
                                                        
                                                            <asp:ImageButton ID="imgHabitacion" runat="server" ImageUrl="~/Img/find16.png" 
                                                            OnClick="imgHabitacion_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                        </td>
                                                        
                                                        <td></td>
                                                    </tr>
                                                </table>
                                           

                                                    <telerik:RadGrid ID="rgAcuerdos_Habitacion" runat="server" AllowPaging="False" AutoGenerateColumns="False"
 GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_Habitacion_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_Habitacion_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_Habitacion_ItemDataBound" OnItemCreated="rgAcuerdos_Habitacion_ItemCreated"
                                                        BorderStyle="None" ShowFooter="true" OnPreRender="rgAcuerdos_Habitacion_PreRender">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd") %>'
                                                                            MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto"
                                                                    UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox  ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px" Enabled="False">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Prd_Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Acys_Cantidad")),Convert.ToDouble(Eval("Prd_Precio"))).ToString("N2")%>'></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                                
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaInicio" HeaderText="Fecha Inicio" UniqueName="Acs_ConsigFechaInicio"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acys_ConsigFechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpConsigFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaInicio") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaFin" HeaderText="Fecha Fin" UniqueName="Acs_ConsigFechaFin"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("Acys_ConsigFechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                      
                                                                         <telerik:RadDatePicker ID="tpConsigFechaFin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaFin") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                               
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("Acys_CantTotal" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" DbValue='<%# Bind("Acys_CantTotal") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                        
                                                    </div>

                                                    <%--grid Iguala--%>


                                              <div id="div_iguala" runat="server">  
                                              <br />
                                                <table>
                                                    <tr>
                                                        <td style="width:200px"><b>GARANTIA POR IGUALA</b></td> <td style="width:100px">&nbsp;</td>

                                                        <td>Factor Garantía</td><td>
                                                        
                                                          <telerik:RadNumericTextBox ID="Fac_Iguala" runat="server" 
                                                                            MinValue="0" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                           </telerik:RadNumericTextBox>

                                                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="Fac_Iguala"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" /> 
                                                        
                                                        </td><td></td>

                                                        <td>U. PRIMA META</td><td>
                                                        
                                                         <telerik:RadNumericTextBox ID="PNeta_Iguala" runat="server" 
                                                                            MinValue="0"  MaxValue="100" MaxLength="9" Width="100%">                                                                            
                                                                            <NumberFormat GroupSeparator="" />
                                                         </telerik:RadNumericTextBox>
                                                        
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="PNeta_Iguala"
                                                            ErrorMessage="*" ValidationGroup="Guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" /> 
                                                        
                                                        </td><td></td>

                                                        <td>Fecha Corte</td>
                                                        <td>
                                                            <asp:ImageButton ID="imgIguala" runat="server" ImageUrl="~/Img/find16.png" 
                                                            OnClick="imgIguala_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>

                                                 <telerik:RadGrid ID="rgAcuerdos_Iguala" runat="server" AllowPaging="False" AutoGenerateColumns="False"
 							GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        OnNeedDataSource="rgAcuerdos_Iguala_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_Iguala_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_Iguala_ItemDataBound" OnItemCreated="rgAcuerdos_Iguala_ItemCreated"
                                                        BorderStyle="None" ShowFooter="true" OnPreRender="rgAcuerdos_Iguala_PreRender">
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPrd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" DbValue='<%# Bind("Id_Prd") %>'
                                                                            MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del producto"
                                                                    UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="170px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="170px"
                                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPresentacion" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblPresentacionEd" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniNom" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblUniEd" runat="server" Text='<%# Bind("Prd_UniNom") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Acys_Cantidad" ) %>'
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox Enabled="False" ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Prd_Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("Prd_Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Acys_Cantidad")),Convert.ToDouble(Eval("Prd_Precio"))).ToString("N2")%>'></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Acys_Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Acys_Frecuencia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acys_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acys_Frecuencia") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLunes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Lunes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Lunes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMartes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Martes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Martes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMiercoles" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Miercoles") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Miercoles")) %>'
                                                                              GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJueves" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Jueves") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Jueves")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkViernes" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Viernes") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Viernes")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSabado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Acys_Sabado") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Acys_Sabado")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_DocStr") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox ID="txtDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                                            Width="80px">
                                                                            <Items>
                                                                               
                                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaInicio" HeaderText="Fecha Inicio" UniqueName="Acs_ConsigFechaInicio"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label109" runat="server" Text='<%# Bind("Acys_ConsigFechaInicio",  "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                        
                                                                         <telerik:RadDatePicker ID="tpConsigFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaInicio") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                                                      
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_ConsigFechaFin" HeaderText="Fecha Fin" UniqueName="Acs_ConsigFechaFin"  Display="false">
                                                                    <HeaderStyle Width="130px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("Acys_ConsigFechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>                                                                      
                                                                         <telerik:RadDatePicker ID="tpConsigFechaFin" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX"   MinnDate="01/01/0001" 
                                                                                DbSelectedDate ='<%# Eval("Acys_ConsigFechaFin") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>                                               
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("Acys_CantTotal" ) %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" DbValue='<%# Bind("Acys_CantTotal") %>'
                                                                            Width="50px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltSCtp" DataField="Acys_UltSCtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="Acys_UltACtp" DataField="Acys_UltACtp" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                                    ConfirmDialogWidth="350px">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>

                                                     </div>



                                                </td>
                                            </tr>
                                        </table>
                                          </div>                                       
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>

            <%--    Edsg 24062015--%>
            <telerik:RadPageView ID="RPCalendario" runat="server" Width="100%"> 
                  <telerik:RadSplitter ID="RadSplitter4" runat="server" Width="100%"  Height="550px"  ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                   <telerik:RadPane ID="RadPane4" runat="server" OnClientResized="onResize">
                <div style="overflow:auto">
                <br />
                <asp:Label ID="lblComboGarantiaCal" runat="server" Text="Tipo de Garantía: "></asp:Label>
                &nbsp;
                    <select onchange="jsFunction()" id="OptGarantia">
                        <option value="-1">- Seleccione -</option>
                        <option value="0">Regular</option>
                        <option value="1">Kilo</option>
                        <option value="2">Comensal</option>
                        <option value="3">Habitacion</option>
                        <option value="4">Iguala</option>
                    </select>


                    <table id="tCal1" runat=server >

                        <tr>
                            <td>                             
                                    <table id="tblCalendario_1" name="tblCalendario_1" class="tblCalendario" runat="server" >
                    
                                    <tr id="encabezado_b1">
                               
                                        <th colspan=5>Enero</th>
                                    </tr>
                 

                                    <thead>
                                    <tr id="encabezado_1">
                                        <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                                    </tr>
                                    </thead>
                                    <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                    </tr>
                                 </table>
                         </td>
                         <td>&nbsp;&nbsp;&nbsp;</td>

                         <td>
                              <table id="tblCalendario_2" name="tblCalendario_2" class="tblCalendario" runat="server" >
                             <tr id="encabezado_b2">
                               <th colspan=5>Febrero</th>
                              </tr>
                            <thead>
                            <tr id="encabezado_2">
                                <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            </table>
                            
                         </td>
                        </tr>

                    </table>    


                   <table  id="tCal2" runat=server>

                        <tr>
                            <td>                             
                             <table id="tblCalendario_3" name="tblCalendario_3" class="tblCalendario" runat="server" >
                             <tr id="encabezado_b3">
                                                    <th colspan=5>Marzo</th>
                                                     </tr>
                            <thead>
                            <tr id="encabezado_3">
                                                           <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                         </td>
                               <td>&nbsp;&nbsp;&nbsp;</td>
                         <td>
                           <table id="tblCalendario_4" name="tblCalendario_4" class="tblCalendario" runat="server" >
                          <tr id="encabezado_4b">
                                                             <th colspan=5>Abril</th>
                             </tr>
                            <thead>
                            <tr id="encabezado_4">
                                                           <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                            
                         </td>
                        </tr>

                    </table>            


                    <table  id=tCal3 runat=server>

                        <tr>
                            <td>                             
                           <table id="tblCalendario_5" name="tblCalendario_5" class="tblCalendario" runat="server" >

                                 <tr id="encabezado_5b">
                                                           <th colspan=5>Mayo</th>
                             </tr>
                            <thead>
                            <tr id="encabezado_5">
                                                       <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                         </td>
                               <td>&nbsp;&nbsp;&nbsp;</td>
                         <td>
                              <table id="tblCalendario_6" name="tblCalendario_6" class="tblCalendario" runat="server" >
                                <tr id="encabezado_6b">
                                 <th colspan=5>Junio</th>
                                </tr>
                            <thead>
                            <tr id="encabezado_6">
                                                           <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                            
                         </td>
                        </tr>

                    </table>    

                        
                     <table  id=tCal4 runat=server>

                        <tr>
                            <td>         
                                <table id="tblCalendario_7" name="tblCalendario_7" class="tblCalendario" runat="server" >
                                                                                     <tr id="encabezado_7b">
                                            <th colspan=5>Julio</th>
                                                                          </tr>
                                    <thead>
                                    <tr id="encabezado_7">
                                                                      <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                                    </tr>
                                    </thead>
                                    <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                    </tr>
                                </table>             

                         </td>
                               <td>&nbsp;&nbsp;&nbsp;</td>
                         <td>
                           <table id="tblCalendario_8" name="tblCalendario_8" class="tblCalendario" runat="server" >
                                                                             <tr id="encabezado_8b">
                                <th colspan=5>Agosto</th>
                            </tr>
                            <thead>
                            <tr id="encabezado_8">
                                  <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                            
                         </td>
                        </tr>

                    </table>    
                   
                     <table  id=tCal5 runat=server>

                        <tr>
                            <td> 
                             <table id="tblCalendario_9" name="tblCalendario_9" class="tblCalendario" runat="server" >
                              <tr id="encabezado_9b">
                                 <th colspan=5>Septiembre</th>
                             </tr>
                            <thead>
                            <tr id="encabezado_9">
                                 <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>                     

                         </td>
                               <td>&nbsp;&nbsp;&nbsp;</td>
                         <td>
                            
                              <table id="tblCalendario_10" name="tblCalendario_10" class="tblCalendario" runat="server" > 
                                                                             <tr id="encabezado_10b">
                                <th colspan=5>Octubre</th>
                            </tr>
                            <thead>
                            <tr id="encabezado_10">
                                <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                            
                         </td>
                        </tr>

                    </table>    

                     <table  id=tCal6 runat=server>

                        <tr>
                            <td>                             
                               <table id="tblCalendario_11" name="tblCalendario_11" class="tblCalendario" runat="server" >
                                                                             <tr id="encabezado_11b">
                                <th colspan=5>Noviembre</th>
                            </tr>
                            <thead>
                            <tr id="encabezado_11">
                                 <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                            </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                         </td>
                               <td>&nbsp;&nbsp;&nbsp;</td>
                         <td>
                               <table id="tblCalendario_12" name="tblCalendario_12" class="tblCalendario" runat="server" >
                                <tr id="encabezado_12b">
                                <th colspan="5">Diciembre</th>

                            </tr>
                            <thead>
                                <tr id="encabezado_12">
                                     <th>Semana 1</th><th>Semana 2</th><th>Semana 3</th><th>Semana 4</th><th>Semana 5</th>
                                </tr>
                            </thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                           </table>
                            
                         </td>
                        </tr>

                    </table>                         
                    <input type="hidden" id="txtValoresCalendario" style="width:300px" runat="server" />
                                            
<%--                    <p></p>   
                    <p></p>   
                    <p></p>   
                                         


                    <input type="button" value="Mi botonsote" id="btnTest" />

                    <div id="div3" ondrop="drop(event)" ondragover="allowDrop(event)" ondragenter="dragEnter(event)" style="border-style:solid;width:400px;height:100px"></div>
                    <br>

                    <div id="div222"  style="border-style:solid;width:200px;height:100px"  >                    
                            <img id="Img1" src="Imagenes/Entregas20.png" ondragstart="drag(event)" draggable="true"> <label>Item 1</label>
                    </div>

                    <br />--%>
                  <%--  <img id="drag1" src="Imagenes/ENTREGA_ALMACEN.png" draggable="true" ondragstart="drag(event)" width="336" height="69">--%>

                  </div>
                </telerik:RadPane>
                </telerik:RadSplitter>
        </telerik:RadPageView>
                   




                            <telerik:RadPageView ID="RPVCondicionesPago" runat="server" Enabled="False">
                                <telerik:RadSplitter ID="RadSplitter5" BorderSize="0" runat="server" Width="99%"
                                    Height="415px" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane5" runat="server" Width="99%" Height="415px" OnClientResized="onResize">
                                    <div runat="server" id="divCondionesPago" style="font-family: verdana; font-size: 8pt">                                    
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
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
                                                    <asp:CheckBox ID="chkCredito" runat="server" Text="Crédito" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Días
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtDias" runat="server" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>                                               
                                                <td>
                                                 Naturales
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
                                                  <td>
                                                <td>
                                                    <asp:Label ID="Label30" runat="server" Text="Límite de crédito"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtLimite" runat="server" Width="80px">
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                      <asp:Label ID="Label131" runat="server" Text="Banco nos deposita al cliente"></asp:Label> 
                                                </td>
                                                <td>
                                                        
                                                       <telerik:RadComboBox ID="cmbCPBancoDeposita" runat="server"  ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" 
                                                             Width="200px" ReadOnly="True"
                                                            MaxHeight="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                  </table>
	                                                    </ItemTemplate>
                                                     </telerik:RadComboBox>

                                                </td>
                                                <td>
                                                    <asp:Label ID="Label132" runat="server" Text="Número de Cuenta"></asp:Label> 
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCPNumCuenta" runat="server" Width="200px" MaxLength="50">
                                                     </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label133" runat="server" Text="Referencia tecleada"></asp:Label> 
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCPRefTecleada" runat="server" Width="200px" MaxLength="50">
                                                     </telerik:RadTextBox>
                                                </td>

                                                <td></td>

                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkContado" runat="server" Text="Contado" />
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
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td colspan="2" width="100">
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="50">
                                                </td>
                                            </tr>                                                       
                                                        
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                </tr>
                                            </table>                                                
                                            <table width="100%">
                                                <tr>
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  5.1 Formas de Pago</th>                                                         
                                                    </tr>
                                                <tr>
                                                    <td width="10">
                                                    </td>
                                                    <td width="20">
                                                    </td>
                                                    <td width="120">
                                                    </td>
                                                </tr>                                                        
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkEfectivo" runat="server" Text="Efectivo" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="ChkDeposito" runat="server" Text="Cheque nominativo" />
                                                    </td>

                                                    <td>
                                                          <asp:Label ID="Label134" runat="server" Text="Unico día"></asp:Label> 
                                                    </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPUnicoDia" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>
                                                   
                                                   <td>
                                                          <asp:Label ID="Label135" runat="server" Text="Dirección"></asp:Label> 
                                                    </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPDiasPagoDireccion" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>
                                                    <td> <asp:Label ID="Label136" runat="server" Text="Colonia"></asp:Label> </td>

                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPColonia" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>

                                                    <td> <asp:Label ID="Label137" runat="server" Text="Municipio"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPMunicipio" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkFactoraje" runat="server" Text="Transferencia electrónica de fondos" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="chkTarjetaDebito" runat="server" Text="Tarjeta de Debito" />
                                                    </td>

                                                  
                                                  <td> <asp:Label ID="Label138" runat="server" Text="Dia máximo"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPDiaMaximo" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>


                                                  <td> <asp:Label ID="Label139" runat="server" Text="Estado"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPEstado" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>


                                                   <td> <asp:Label ID="Label140" runat="server" Text="C.P."></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPCP" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>

                                                    <td> <asp:Label ID="Label141" runat="server" Text="Ciudad"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPCiudad" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>



                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>                                                            
                                                    <td>
                                                        <asp:CheckBox ID="chkTransferencia" runat="server" Text="Monedero electrónico" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="ChkTarjetaCredito" runat="server" Text="Dinero electrónico" />
                                                    </td>
                                                    
                                                    
                                                    <td> <asp:Label ID="Label142" runat="server" Text="Persona de cuentas por pagar"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPCuentasPagar" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>

                                                 
                                                   <td> <asp:Label ID="Label143" runat="server" Text="Telefonos"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPTelefonos" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>


                                                </tr>
                                                <tr>                                                            
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkCheque" runat="server" Text="Vales de despensa" />
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                              
                                                   <td> <asp:Label ID="Label144" runat="server" Text="Ultimos 4 dig. Cuenta de Pago"></asp:Label> </td>
                                                    <td>
                                                    
                                                         <telerik:RadTextBox ID="txtCPCuentaPago" runat="server" Width="200px" MaxLength="50">
                                                         </telerik:RadTextBox>
                                                    </td>

                                                </tr>                                                        
                                            </table>




                                        <table width="100%">
                                        <tr>
                                            <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  5.2 Revisión de Facturas</th>                                                         
                                         </tr>  
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td height="10" width="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label321" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label34" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label35" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label36" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label37" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label38" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label33" runat="server" Text="Días de revisión"></asp:Label>
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionLunes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionMartes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionMiercoles" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionJueves" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionViernes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chkRevisionSabado" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>                                                        
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="120">
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
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label51" runat="server" Text="Horarios de revisión"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionMañanaInicio" runat="server" Culture="es-MX"
                                                                    Width="100px">
                                                                    <Calendar ID="Calendar4" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionMañanaFin" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar5" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionTardeInicio" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar6" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView3" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpRevisionTardeFin" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar7" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView4" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput7" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label54" runat="server" Text="Documentos Adicionales"></asp:Label>
                                                              </td>   
                                                              <td colspan="5"></td>                                                                                                                   
                                                                                                                                                               
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td valign="top" width="100">
                                                            </td>                                                            
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkRevFolio" runat="server" Text="Folio" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkRevRntrada" runat="server" Text="Entrada de Almacén"/>
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkRevOrden" runat="server" Text="Orden de Compra" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkRevReporte" runat="server" Text="Reporte de Consumo" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkRevCopia" runat="server" Text="Copia de Factura" />
                                                            </td>                                                                                                                       
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td valign="top" width="100">
                                                                <asp:Label ID="Label32" runat="server" Text="Otro"></asp:Label>
                                                              </td>  
                                                            <td colspan="4">
                                                             <telerik:RadTextBox ID="txtrevOtro" runat="server"  width="600">
                                                             </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                    <tr>
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  5.3 Pago de Facturas</th>                                                         
                                                     </tr>
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
                                                            </td>
                                                            <td width="50" align="center">
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
                                                            <td width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label39" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label40" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label41" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label43" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label45" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:Label ID="Label47" runat="server" Text="Sábado"></asp:Label>
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
                                                                <asp:Label ID="Label52" runat="server" Text="Días de pago"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoLunes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoMartes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoMiercoles" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoJueves" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoViernes" runat="server" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkPagoSabado" runat="server" />
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
                                                    </table>
                                                    <table >
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="115">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td width="100">
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td width="100">
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
                                                                <asp:Label ID="Label53" runat="server" Text="Horarios de pago"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoMañanaInicio" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar8" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton runat="server" CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView5" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton runat="server" CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput8" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label58" runat="server" Text="a"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoMañanaFin" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar9" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView6" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput9" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label57" runat="server" Text="y"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoTardeInicio" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar10" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView7" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput10" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label56" runat="server" Text="a"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="tpPagoTardeFin" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar11" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView8" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput11" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                               <table>
                                                        <tr>
                                                            <td>Presencial</td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:CheckBox runat="server" ID="chkRevFacContraEntrega" Text="Contra Entrega" /></td>
                                                            <td><asp:CheckBox runat="server" ID="chkRevFacVisGestorCobranza" Text="Visita del Gestor de Cobranza" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>     <asp:Label ID="Label180" runat="server" Text="Dirección"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacDireccion" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                          <td>     <asp:Label ID="Label181" runat="server" Text="Colonia"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacColonia" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>

                                                             <td>     <asp:Label ID="Label182" runat="server" Text="Municipio"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacMunicipio" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>     <asp:Label ID="Label183" runat="server" Text="Estado"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacEstado" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                          <td>     <asp:Label ID="Label184" runat="server" Text="CP"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacCP" runat="server" Width="50px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>

                                                             <td>     <asp:Label ID="Label185" runat="server" Text="Ciudad"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacCiudad" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                        </tr>
                                                    
                                                       <tr>
                                                            <td>     <asp:Label ID="Label186" runat="server" Text="Teléfonos"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacTeléfonos" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>


                                                        </tr>

                                                           <tr>
                                                            <td cospan="2"><asp:Label ID="Label211" runat="server" Text="Eléctronica&nbsp;:&nbsp;"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        </tr>

                                                           <tr>
                                                            <td> <asp:CheckBox runat="server" ID="chk" Text="Email" />    </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacEmailTexto" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>

                                                        </tr>

                                                      <tr>
                                                             <td> <asp:CheckBox runat="server" ID="chkRevFacPortal" Text="Portal" />    </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacPortalTexto" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                          <td>     <asp:Label ID="Label188" runat="server" Text="http://"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacHttp" runat="server" Width="50px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>

                                                             <td>     <asp:Label ID="Label189" runat="server" Text="Usuario:"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacUsuario" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>

                                                            <td>     <asp:Label ID="Label187" runat="server" Text="Contraseña::"></asp:Label> </td>
                                                            <td><telerik:RadTextBox ID="txtRevFacContrasenia" runat="server" Width="150px" MaxLength="50">
                                                                     </telerik:RadTextBox></td>
                                                        </tr>


                                                    </table>

                                                </td>

                                            </tr>
                                        </table>         
                                
                                        <table>
                                                        <tr>
                                                            <td><asp:Label ID="Label190" runat="server" Text="Documentos" /></td>
                                                            <td><asp:Label ID="Label191" runat="server" Text="Entrega" /></td>
                                                            <td><asp:Label ID="Label192" runat="server" Text="No. Copias" /></td>
                                                            <td><asp:Label ID="Label193" runat="server" Text="Recepción" /></td>
                                                            <td><asp:Label ID="Label194" runat="server" Text="No.Copias" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label195" runat="server" Text="Factura Franquicia" /></td>
                                                            <td><asp:CheckBox ID="chkCPFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactFranquiciaEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactFranquiciaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label196" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkCPFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactKeyEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactKeyRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label197" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkCPOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdCompraEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdCompraRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label198" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkCPOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdReposEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdReposRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label199" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkCPCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPCopPedidoEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPCopPedidoRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label200" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkCPRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRemisionEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRemisionRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label201" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkCPFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFolioEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFolioRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label202" runat="server" Text="Contra recibo" /></td>
                                                            <td><asp:CheckBox ID="chkCPContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPContraRecEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPContraRecRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label203" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkCPEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPEntAlmacenEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPEntAlmacenRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label204" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkCPSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPSopServicioEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPSopServicioRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label205" runat="server" Text="Nombre y Firma de recibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkCPNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPNomFirmaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPNomFirmaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label206" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkCPRecCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRecCitaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPRecCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRecCitaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
    
                                                </tr>
                                            </table>
                                
                                
                                        
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>     
                            <telerik:RadPageView ID="RPVServicio" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter6" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true" Height="615px">                                 
                                    <telerik:RadPane ID="RadPane6" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="divServicio" style="font-family: verdana; font-size: 8pt">                                    

                                        <table Width="100%">
                                          <tr>
                                          <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="8"  >6.1 Visita del Representante</th>
                                          </tr>
                                          </table>
                                        <table>
                                            <tr>
                                            <td></td>
                                             <td>
                                                Frecuencia
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="Vis_Frecuencia" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="4">
                                                    Vez(Veces) al mes
                                                </td>
                                                <td colspan="3"></td>
                                                  <td><asp:Label ID="Label42" runat="server" Text="Otro:"/></td>
                                                    <td colspan="2"  >
                                                        <telerik:RadTextBox ID="txtVisitaOtro" runat="server" ReadOnly="False" width="300">
                                                        </telerik:RadTextBox> 
                                                     </td> 
                                            </tr>
                                            </table>


                                        <table Width="100%">
                                          <tr>
                                          <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="8"  >6.2 Servicio de Asesoria</th>
                                          </tr>
                                          </table>

                                                    <table>
                                                        <tr>
                                                            <td height="10" width="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label4862" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label4962" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label5062" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label21262" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label21362" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label21462" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label21562" runat="server" Text="Días de recepción"></asp:Label>
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Lunes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Martes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Miercoles" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Jueves" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Viernes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk62Sabado" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>                                                        
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="120">
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
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label21662" runat="server" Text="Horarios de recepción"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker162" runat="server" Culture="es-MX"
                                                                    Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1462" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView962" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1462" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker262" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar15" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1062" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1562" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker362" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1662" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1162" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1662" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker462" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1762" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1262" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1762" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label21762" runat="server" Text="Persona que recibe: "></asp:Label>
                                                              </td>   
                                                              <td  colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPersonaRecibe62" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                              <td valign="top" width="100">
                                                                <asp:Label ID="Label21862" runat="server" Text="Puesto: "></asp:Label>
                                                              </td>   
                                                              <td colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPuesto62" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                                                              
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label21962" runat="server" Text="Cita para entrega: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="Chk62Mismodia" runat="server" Text="Mismo día" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="Chk62Sincita" runat="server" Text="Sin cita" /></td>    
                                                              <td colspan="2"><asp:CheckBox ID="Chk62Previa" runat="server" Text="Previa" /></td>  
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Contacto:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txt62CitaContacto" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td colspan="6"></td>
                                                            <td>Teléfono:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txt62CitaTelefono" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Dias de Anticipación:</td>
                                                            <td>     

                                                                        <telerik:RadNumericTextBox ID="txt62CitaDiasdeAnticipacion" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label22062" runat="server" Text="Área de recibo: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chk62AreaPropia" runat="server" Text="Propia" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chk62AreaPlaza" runat="server" Text="Plaza" /></td>    
                                                              <td colspan="1"><asp:CheckBox ID="chk62AreaCalle" runat="server" Text="Calle" /></td>  
                                                              <td ><asp:CheckBox ID="chk62AreaAvTransitada" runat="server" Text="Avenida transitada" /></td>  
                                                        </tr>
                                                       <tr>
                                                              <td colspan="2"><asp:Label ID="Label22162" runat="server" Text="Estacionamiento: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chk62EstCortesia" runat="server" Text="Cortesía" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chk62EstCosto" runat="server" Text="Costo" /></td> 
                                                              <td colspan="1"><asp:Label ID="Label22262" runat="server" Text="Monto: " /></td>   
                                                              <td >
                                                                    <telerik:RadNumericTextBox ID="txt62EstMonto" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                              </td>  

                                                        </tr>



                                                 <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label4862a" runat="server" Text="Dirección"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txt62ClienteDireccion" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label4962A" runat="server" Text="Colonia"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txt62ClienteColonia" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                  
                                                   <td colspan="1">  <asp:Label ID="Label5062A" runat="server" Text="Municipio"></asp:Label>         </td> 
                                                      <td colspan="6">
                                                        <telerik:RadTextBox ID="txt62ClienteMunicipio" runat="server" ReadOnly="False" width="410">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                     
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label21262A" runat="server" Text="Estado"/></td>                                                  
                                                    <td colspan="2">
                                                        <telerik:RadTextBox ID="txt62ClienteEstado" runat="server" ReadOnly="False" width="185" >
                                                        </telerik:RadTextBox>                       </td>                                                                                                                                                              
                                                    <td> <asp:Label ID="Label21362A" runat="server" Text="C.P." style="text-align:center;"  width="30" ></asp:Label> </td> 
                                                    <td colspan="1">
                                                        <telerik:RadTextBox ID="txt62ClienteCodPost" runat="server" ReadOnly="False" width="47">
                                                        </telerik:RadTextBox> </td>
                                                    <td colspan="1">&nbsp;</td>
                                                    <td colspan="6">&nbsp;</td> 
                                                    <td></td>   
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>                                                    
                                                    <td>&nbsp;</td>                                                        
                                                    <td colspan="4">&nbsp;</td>   

                                                </tr>

                                                        </table>





                                             <table>
                                            <tr>
                                                <td>
                                                </td>                                                            
                                                <td>
                                                    <asp:CheckBox ID="ChkServAsesoria" runat="server" OnCheckedChanged="ChkServAsesoria_CheckedChanged" Text="Requiere Servicio de Asesoria" 
                                                        AutoPostBack="true" Checked="True" />
                                                </td>                                                            
                                             </tr>
                                             <tr>
                                                <td width="10"></td>                                           
                                              </tr>
                                                <tr id="AsesoriaListado" runat="server">
                                                    <td width="10">  </td>
                                                    <td>
                                                        <telerik:RadGrid ID="rgAsesoria" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                            OnNeedDataSource="rgAsesoria_NeedDataSource" PageSize="15">
                                                            <MasterTableView>
                                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="Id_Ase" HeaderText="Núm." UniqueName="Id_Ase">
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Ase_Descripcion" HeaderText="Descripción" UniqueName="Ase_Descripcion">
                                                                        <HeaderStyle Width="500px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                        <HeaderTemplate>
                                                          <table id="Table6" cellspacing="1" cellpadding="1" width="600" >
                                                            <tr>
                                                              <td colspan="6" align="center">
                                                                <b>Frecuencia</b>
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                              <td width="12%">
                                                                <b>Mensual</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                               <td width="12%">
                                                                <b>Bimestral</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                               <td width="12%">
                                                                <b>Trimestral</b>
                                                              </td>
                                                              <td width="21%">
                                                                <b>Fecha Inicio</b>
                                                              </td>
                                                            </tr>
                                                          </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                          <table id="Table7" cellspacing="1" cellpadding="1" width="600" border="1">
                                                            <tr>
                                                              <td width="12%">                                                          
                                                                  <asp:RadioButton ID="ServAsesoriaMensual" GroupName="FrecuenciaServAsesoria" runat="server"  OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                 <telerik:RadDatePicker ID="ServAsesoriaMensualfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                    AutoPostBack="True" Culture="es-MX" Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaMensual")) %>'  MinnDate="01/01/0001" 
                                                                     DbSelectedDate ='<%# Eval("Ase_ServAsesoriaMensualfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>
                                                              <td width="12%"> 
                                                                <asp:RadioButton ID="ServAsesoriaBimestral" GroupName="FrecuenciaServAsesoria" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                <telerik:RadDatePicker ID="ServAsesoriaBimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged" 
                                                                    AutoPostBack="True" Culture="es-MX"  Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaBimestral")) %>' MinDate="01/01/0001" 
                                                                    DbSelectedDate ='<%# Eval("Ase_ServAsesoriaBimestralfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal"  />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>
                                                               <td width="12%">
                                                                <asp:RadioButton ID="ServAsesoriaTrimestral" GroupName="FrecuenciaServAsesoria" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"  Checked='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral")) %>'/>
                                                              </td>
                                                              <td width="21%">
                                                                <telerik:RadDatePicker ID="ServAsesoriaTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged" 
                                                                    AutoPostBack="True" Culture="es-MX" Enabled='<%# DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Ase_ServAsesoriaTrimestral")) %>' MinDate="01/01/0001" DbSelectedDate ='<%# Eval("Ase_ServAsesoriaTrimestralfechaIni") %>'>
                                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x" ShowRowHeaders="false">
                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                            TodayButtonCaption="Hoy" />
                                                                    </Calendar>
                                                                    <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                </telerik:RadDatePicker>
                                                              </td>                                                       
                                                            </tr>
                                                          </table>
                                                        </ItemTemplate>
                                                      </telerik:GridTemplateColumn>
                                                      </Columns>
                                                </MasterTableView>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                                NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                                PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>                                           
                                                  <tr>
                                                    <td> <asp:Label ID="Label107" runat="server" Text="  "></asp:Label></td>
                                                    <td>
                                                      <table>
                                                        <tr>
                                                            <td><asp:Label ID="Label146" runat="server" Text="Documentos" /></td>
                                                            <td><asp:Label ID="Label147" runat="server" Text="Entrega" /></td>
                                                            <td><asp:Label ID="Label148" runat="server" Text="No. Copias" /></td>
                                                            <td><asp:Label ID="Label149" runat="server" Text="Recepción" /></td>
                                                            <td><asp:Label ID="Label150" runat="server" Text="No.Copias" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label151" runat="server" Text="Factura Franquicia" /></td>
                                                            <td><asp:CheckBox ID="chk62DocFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFactFranquiciaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFactFranquiciaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label152" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chk62DocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFactKeyEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFactKeyRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label153" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chk62DocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocOrdCompraEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocOrdCompraRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label154" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chk62DocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocOrdReposEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocOrdReposRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label155" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chk62DocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocCopPedidoEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocCopPedidoRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label156" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chk62DocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocRemisionEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocRemisionRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label157" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chk62DocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFolioEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocFolioRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label158" runat="server" Text="Contra ServAseibo" /></td>
                                                            <td><asp:CheckBox ID="chk62DocContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocContraRecEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocContraRecRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label159" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chk62DocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocEntAlmacenEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocEntAlmacenRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>


                                                <tr>
                                                            <td><asp:Label ID="Label160" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chk62DocSopServicioEnt" runat="server" /></td>
                                                          <td>    <telerik:RadNumericTextBox ID="txt62DocSopServicioEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                           </td>
                                                           <td><asp:CheckBox ID="chk62DocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocSopServicioRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                            </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label161" runat="server" Text="Nombre y Firma de ServAseibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chk62DocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocNomFirmaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62DocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62DocNomFirmaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label162" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chk62CitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62CitaEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk62CitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt62CitaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
    
                                                </tr>
                                            </table>
                                                    
                                                    
                                                    </td>
                                                 </tr>   
                                        </table>
                                        <table Width="100%">
                                          <tr>
                                          <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="8"  >6.3 Servicio Técnico</th>
                                          </tr>
                                          </table>




                                                    <table>
                                                        <tr>
                                                            <td height="10" width="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label48" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label49" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label50" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label212" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label213" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label214" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label215" runat="server" Text="Días de recepción"></asp:Label>
                                                            </td>
                                                            <td width="10">
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Lunes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Martes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Miercoles" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Jueves" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Viernes" runat="server" />
                                                            </td>
                                                            <td align="center" width="50">
                                                                <asp:CheckBox ID="chk63Sabado" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table>                                                        
                                                        <tr>
                                                            <td width="10">
                                                            </td>
                                                            <td width="120">
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
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label216" runat="server" Text="Horarios de recepción"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="Rad63TimePicker163" runat="server" Culture="es-MX"
                                                                    Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1463" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView963" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1463" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="Rad63TimePicker263" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1563" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1063" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1563" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="Rad63TimePicker363" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1663" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1163" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1663" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="Rad63TimePicker463" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <Calendar ID="Calendar1763" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView1263" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput1763" runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label217" runat="server" Text="Persona que recibe: "></asp:Label>
                                                              </td>   
                                                              <td  colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPersonaRecibe63" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                              <td valign="top" width="100">
                                                                <asp:Label ID="Label218" runat="server" Text="Puesto: "></asp:Label>
                                                              </td>   
                                                              <td colspan="4">
                                                                     <telerik:RadTextBox ID="txtRecPuesto63" runat="server" ReadOnly="False" width="200">
                                                                     </telerik:RadTextBox>
                                                              
                                                              </td>
                                                                                              
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label219" runat="server" Text="Cita para entrega: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="Chk63Mismodia" runat="server" Text="Mismo día" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="Chk63Sincita" runat="server" Text="Sin cita" /></td>    
                                                              <td colspan="2"><asp:CheckBox ID="Chk63Previa" runat="server" Text="Previa" /></td>  
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Contacto:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txt63CitaContacto" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td colspan="6"></td>
                                                            <td>Teléfono:</td>
                                                            <td>     
                                                                    <telerik:RadTextBox ID="txt63CitaTelefono" runat="server" ReadOnly="False" width="200">
                                                                    </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6"></td>
                                                            <td>Dias de Anticipación:</td>
                                                            <td>     

                                                                        <telerik:RadNumericTextBox ID="txt63CitaDiasdeAnticipacion" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                              <td colspan="2"><asp:Label ID="Label220" runat="server" Text="Área de recibo: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chk63AreaPropia" runat="server" Text="Propia" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chk63AreaPlaza" runat="server" Text="Plaza" /></td>    
                                                              <td colspan="1"><asp:CheckBox ID="chk63AreaCalle" runat="server" Text="Calle" /></td>  
                                                              <td ><asp:CheckBox ID="chk63AreaAvTransitada" runat="server" Text="Avenida transitada" /></td>  
                                                        </tr>
                                                       <tr>
                                                              <td colspan="2"><asp:Label ID="Label221" runat="server" Text="Estacionamiento: "></asp:Label></td>
                                                              <td colspan="2"><asp:CheckBox ID="chk63EstCortesia" runat="server" Text="Cortesía" /></td>                                                                                                                   
                                                              <td colspan="2"><asp:CheckBox ID="chk63EstCosto" runat="server" Text="Costo" /></td> 
                                                              <td colspan="1"><asp:Label ID="Label222" runat="server" Text="Monto: " /></td>   
                                                              <td >
                                                                    <telerik:RadNumericTextBox ID="txt63EstMonto" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                              </td>  

                                                        </tr>



                                                 <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label223" runat="server" Text="Dirección"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txt63ClienteDireccion" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label224" runat="server" Text="Colonia"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txt63ClienteColonia" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                  
                                                   <td colspan="1">  <asp:Label ID="Label225" runat="server" Text="Municipio"></asp:Label>         </td> 
                                                      <td colspan="6">
                                                        <telerik:RadTextBox ID="txt63ClienteMunicipio" runat="server" ReadOnly="False" width="410">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                     
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label226" runat="server" Text="Estado"/></td>                                                  
                                                    <td colspan="2">
                                                        <telerik:RadTextBox ID="txt63ClienteEstado" runat="server" ReadOnly="False" width="185" >
                                                        </telerik:RadTextBox>                       </td>                                                                                                                                                              
                                                    <td> <asp:Label ID="Label227" runat="server" Text="C.P." style="text-align:center;"  width="30" ></asp:Label> </td> 
                                                    <td colspan="1">
                                                        <telerik:RadTextBox ID="txt63ClienteCodPost" runat="server" ReadOnly="False" width="47">
                                                        </telerik:RadTextBox> </td>
                                                    <td colspan="1">&nbsp;</td>
                                                    <td colspan="6">&nbsp;</td> 
                                                    <td></td>   
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>                                                    
                                                    <td>&nbsp;</td>                                                        
                                                    <td colspan="4">&nbsp;</td>   

                                                </tr>

                                                        </table>



                                        <table width="95%">    <%--INICIO--%>
                                         <tr>
                                            <td width="10"></td>
                                            <td>    <asp:Label ID="Label103" runat="server" Text="A) Equipos de Servicio (Relleno)"></asp:Label>    </td>
                                          </tr>
                                          <tr>
                                            <td>
                                            </td>                                                            
                                            <td>
                                                <asp:CheckBox ID="ChkServTecnicoRelleno" runat="server" Text="Requiere Servicio a equipos(Relleno) " OnCheckedChanged="ChkServTecnicoRelleno_CheckedChanged" AutoPostBack="True"  Checked="True" />
                                            </td>                                                            
                                         </tr>                                            
                                            <tr id="EquipoRellenoListado" runat="Server">
                                                <td width="10">       </td>
                                                <td>
                                                    <telerik:RadGrid ID="rgServicios" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."                                                      
                                                        OnItemDataBound="rgServicios_ItemDataBound" OnNeedDataSource="rgServicios_NeedDataSource"
                                                        OnItemCommand="rgServicios_ItemCommand" PageSize="15">
                                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd,Prd_AgrupadoSpo" EditMode="InPlace" >
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                             <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd">
                                                                <ItemTemplate>
	                                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
	                                                                <telerik:RadNumericTextBox ID="txtCodigoEdit" runat="server" Text='<%# Bind("Id_Prd") %>' 
                                                                    Width="100px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
	                                                                <ClientEvents OnBlur="ObtenerNombreInicio" OnLoad="onLoadIdPrd" />
                                                                </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoEdit" runat="server" Enabled="false" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="onLoadPrdDescr" />
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="400px" />
                                                                </telerik:GridTemplateColumn>                                                                
                                                                <telerik:GridTemplateColumn DataField="Prd_AgrupadoSpo" Display="false" HeaderText="Prd_AgrupadoSpo"
                                                                    UniqueName="Prd_AgrupadoSpo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAgrupadoSpo" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtAgrupadoSpoEdit" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_InvInicial" HeaderText="Cantidad" UniqueName="Prd_InvInicial">
                                                                    <ItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%"  Enabled="false">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                <HeaderTemplate>
                                                                  <table id="Table2" cellspacing="1" cellpadding="1" width="600" >
                                                                    <tr>
                                                                      <td colspan="6" align="center">
                                                                        <b>Frecuencia</b>
                                                                      </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td width="12%">
                                                                        <b>Mensual</b>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <b>Fecha Inicio</b>
                                                                      </td>
                                                                      <td width="12%">
                                                                        <b>Bimestral</b>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <b>Fecha Inicio</b>
                                                                      </td>
                                                                       <td width="12%">
                                                                        <b>Trimestral</b>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <b>Fecha Inicio</b>
                                                                      </td>                                                       
                                                                    </tr>
                                                                  </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                  <table id="Table3" cellspacing="1" cellpadding="1" width="600" border="1">
                                                                    <tr>
                                                                    <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoMensual"  Enabled="false"  GroupName="FrecuenciaServRelleno" runat="server"  
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoMensual")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoMensualfechaIni" runat="server" Width="100px"
                                                                             Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoMensualfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoBimestral"  Enabled="false"  GroupName="FrecuenciaServRelleno" runat="server"  
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoBimestralfechaIni" runat="server" Width="100px"
                                                                             Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoBimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">
                                                                        <asp:RadioButton ID="ServTecnicoRellenoTrimestral" GroupName="FrecuenciaServRelleno" runat="server"   Enabled="false"
                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <telerik:RadDatePicker ID="ServTecnicoRellenoTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                             Culture="es-MX" Enabled="false" MinDate="01/01/0001"  
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoTrimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>                                                     
                                                                    </tr>
                                                                  </table>
                                                                </ItemTemplate>
                                                                 <EditItemTemplate>
                                                                 <table id="Table8" cellspacing="1" cellpadding="1" width="600" border="1">
                                                                    <tr>
                                                                    <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoMensualEdit" GroupName="FrecuenciaServRellenoEdit" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoMensual")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoMensualEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoMensualfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">                                                          
                                                                          <asp:RadioButton ID="ServTecnicoRellenoBimestralEdit" GroupName="FrecuenciaServRellenoEdit" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"
                                                                          Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoBimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                         <telerik:RadDatePicker ID="ServTecnicoRellenoBimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoBimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>
                                                                      <td width="12%">
                                                                        <asp:RadioButton ID="ServTecnicoRellenoTrimestralEdit" GroupName="FrecuenciaServRellenoEdit" runat="server" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True" 
                                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServTecnicoRellenoTrimestral")) %>'/>
                                                                      </td>
                                                                      <td width="21%">
                                                                        <telerik:RadDatePicker ID="ServTecnicoRellenoTrimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                                            AutoPostBack="True" Culture="es-MX" Enabled="false" MinDate="01/01/0001" 
                                                                             DbSelectedDate ='<%# Eval("ServTecnicoRellenoTrimestralfechaIni") %>'>
                                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                    TodayButtonCaption="Hoy" />
                                                                            </Calendar>
                                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                        </telerik:RadDatePicker>
                                                                      </td>                                                     
                                                                    </tr>
                                                                  </table>     
                                                                  </EditItemTemplate>                                                                 
                                                              </telerik:GridTemplateColumn>
                                                              <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </telerik:GridEditCommandColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                                ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Borrar este detalle?"
                                                                Text="Borrar" UniqueName="DeleteColumn">
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                    Width="30px" />
                                                            </telerik:GridButtonColumn>
                                                    </Columns>
                                                   
                                                </MasterTableView>
                                                <ClientSettings>
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                    NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                    PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                                </td>                                         
                                            </tr>
                                        </table>        <%--FIN--%>
                                             <br />
                                        <table  width="95%">    <%--INICIO--%>
                                         <tr>
                                            <td width="10">  </td>
                                            <td>    <asp:Label ID="Label108" runat="server" Text="B) Mantenimiento Preventivo/Revisión"></asp:Label>    </td>
                                             </tr>
                                        <tr>                                          
                                            <td width="10">  </td>
                                            <td> <asp:CheckBox ID="ChkServMantenimiento" runat="server" Text="Requiere Servicio Mantenimiento Preventivo/Revisión"   OnCheckedChanged="ChkServMantenimiento_CheckedChanged" AutoPostBack="True" Checked="True" /></td>
                                        </tr>                                              
                                         <tr id="MantenimientoPreventivoListado" runat="Server">
                                                <td width="10"></td>
                                                <td>
                                                    <telerik:RadGrid ID="rgMantPrevRev" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."                                                        
                                                        OnItemDataBound="rgMantPrevRev_ItemDataBound" OnNeedDataSource="rgMantPrevRev_NeedDataSource"
                                                        PageSize="15"  OnItemCommand="rgMantPrevRev_ItemCommand" >
                                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd" EditMode="InPlace">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />                                                                
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCodigoEdit" runat="server" Text='<%# Bind("Id_Prd") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <ClientEvents OnBlur="ObtenerNombreInicio" OnLoad="onLoadIdPrd" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label101" runat="server" Text='<%# Bind("Prd_Descripcion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtProductoEdit" runat="server" Enabled="false" Text='<%# Bind("Prd_Descripcion") %>'
                                                                            Width="100%">
                                                                            <ClientEvents OnLoad="onLoadPrdDescr" />
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="400px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_AgrupadoSpo" Display="false" HeaderText="Prd_AgrupadoSpo"
                                                                    UniqueName="Prd_AgrupadoSpo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label82" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtAgrupadoSpoEdit" runat="server" Text='<%# Bind("Prd_AgrupadoSpo") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn DataField="Prd_InvInicial" HeaderText="Cantidad" UniqueName="Prd_InvInicial">
                                                                    <ItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%" Enabled="false">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("Prd_InvInicial") %>'
                                                                            Width="100%">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                <HeaderTemplate>
                                                  <table id="Table4" cellspacing="1" cellpadding="1" width="600" >
                                                    <tr>
                                                      <td colspan="6" align="center">
                                                        <b>Frecuencia</b>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td width="12%">
                                                        <b>Mensual</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>
                                                       <td width="12%">
                                                        <b>Bimestral</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>     
                                                      <td width="12%">
                                                        <b>Trimestral</b>
                                                      </td>
                                                      <td width="21%">
                                                        <b>Fecha Inicio</b>
                                                      </td>                                                       
                                                    </tr>
                                                  </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                  <table id="Table5" cellspacing="1" cellpadding="1" width="600" border="1">
                                                    <tr>
                                                      <td width="12%">
                                                         <asp:RadioButton ID="ServMantenimientoMensual" runat="server" Text="" GroupName="FrecuenciaMantenimiento" Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                         <telerik:RadDatePicker ID="ServMantenimientoMensualfechaIni" runat="server" Width="100px" 
                                                            Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoMensualfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoBimestral" runat="server" Text=""  GroupName="FrecuenciaMantenimiento"  Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoBimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoBimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoTrimestral" runat="server" Text="" GroupName="FrecuenciaMantenimiento"  Enabled = "false"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoTrimestralfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoTrimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                    </tr>
                                                  </table>
                                                </ItemTemplate>
                                                 <EditItemTemplate>
                                                  <table id="Table9" cellspacing="1" cellpadding="1" width="600" border="1">
                                                    <tr>
                                                      <td width="12%">
                                                         <asp:RadioButton ID="ServMantenimientoMensualEdit" runat="server" Text="" GroupName="FrecuenciaMantenimiento" AutoPostBack="True" OnCheckedChanged="Rb_CheckedChanged" 
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoMensual")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                         <telerik:RadDatePicker ID="ServMantenimientoMensualEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoMensualfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoBimestralEdit" runat="server" Text=""  GroupName="FrecuenciaMantenimiento" AutoPostBack="True" OnCheckedChanged="Rb_CheckedChanged"
                                                        Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoBimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoBimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoBimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                      <td width="12%">
                                                        <asp:RadioButton ID="ServMantenimientoTrimestralEdit" runat="server" Text="" GroupName="FrecuenciaMantenimiento" OnCheckedChanged="Rb_CheckedChanged" AutoPostBack="True"
                                                         Checked='<%# DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ServMantenimientoTrimestral")) %>'/>
                                                      </td>
                                                      <td width="21%">
                                                        <telerik:RadDatePicker ID="ServMantenimientoTrimestralEditfechaIni" runat="server" Width="100px" OnSelectedDateChanged="ValidarFechaInicio_SelectedDateChanged"
                                                            AutoPostBack="True" Culture="es-MX" Enabled="false"  DbSelectedDate ='<%# Eval("ServMantenimientoTrimestralfechaIni") %>'>
                                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                            </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        </telerik:RadDatePicker>
                                                      </td>
                                                    </tr>
                                                  </table>
                                                </EditItemTemplate>
                                              </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                    InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                        ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Borrar este detalle?"
                                                        Text="Borrar" UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                    </Columns>
                                                           
                                                        </MasterTableView>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                            NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                            PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                            PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                    </telerik:RadGrid>
                                                </td>                                         
                                            </tr>
                                             <tr>
                                                <td>&#160;</td>
                                                <td>              <table>
                                                        <tr>
                                                            <td><asp:Label ID="Label163" runat="server" Text="Documentos" /></td>
                                                            <td><asp:Label ID="Label164" runat="server" Text="Entrega" /></td>
                                                            <td><asp:Label ID="Label165" runat="server" Text="No. Copias" /></td>
                                                            <td><asp:Label ID="Label166" runat="server" Text="ServTecepción" /></td>
                                                            <td><asp:Label ID="Label167" runat="server" Text="No.Copias" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label168" runat="server" Text="Factura Franquicia" /></td>
                                                            <td><asp:CheckBox ID="chk63DocFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFactFranquiciaEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFactFranquiciaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label169" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chk63DocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFactKeyEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFactKeyRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label170" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chk63DocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocOrdCompraEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocOrdCompraRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label171" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chk63DocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocOrdReposEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocOrdReposRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label172" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chk63DocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocCopPedidoEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocCopPedidoRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label173" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chk63DocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocRemisionEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocRemisionRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label174" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chk63DocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFolioEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocFolioRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label175" runat="server" Text="Contra ServTecibo" /></td>
                                                            <td><asp:CheckBox ID="chk63DocContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocContraRecEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocContraRecRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label176" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chk63DocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocEntAlmacenEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocEntAlmacenRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label177" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chk63DocSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocSopServicioEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocSopServicioRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label178" runat="server" Text="Nombre y Firma de ServTecibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chk63DocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocNomFirmaEntCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63DocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63DocNomFirmaRecCop" runat="server" MaxLength="9" 
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label179" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chk63CitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txt63CitaEntCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chk63CitaRec" runat="server" /></td>
                                                           <td>    <telerik:RadNumericTextBox ID="txt63CitaRecCop" runat="server" MaxLength="9"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                           </td>
    
                                                </tr>
                                            </table></td>                                            </tr>
                                        </table>
                                            <br />
                                        <br />
                                        <br />
                                        <br />
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>                     

                            <telerik:RadPageView ID="RPVOtrosApoyos" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter7" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane7" runat="server" OnClientResized="onResize">
                                    <div runat="server" id="divOtrosApoyos" style="font-family: verdana; font-size: 8pt">  
                                            <table width="100%">                                                
                                                <tr>
                                                    <td width="10">  </td>
                                                    <td>Notas:  </td>
                                                    <td colspan = "6">
                                                        <asp:TextBox id="txtNotas" TextMode="multiline" Columns="170" Rows="8" runat="server" />
                                                    </td>
                                                </tr>                                                  
                                            </table>
                                              <table width="100%"> 
                                            <thead >                                                        
                                                 <th style="font-family:  verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;"   >  PERSONAL DE KEY QUE LO PUEDE ANTENDER </th>  
                                            </thead> 
                                            </table>

                                            <table>                                               
                                                <tr>
                                                    <td width="10">  </td>
                                                    <td> <asp:Label ID="Label31" runat="server" Text="Representante de Ventas" /></td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoRepVenta" runat="server" Width="250px" AutoPostBack="True"
                                                        Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                     <%--   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" ControlToValidate="ContactoRepVenta"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  --%>                                                  
                                                     </td>
                                                    <td></td>
                                                    <td> <asp:Label ID="Label66" runat="server" Text="Teléfono" /> </td>

                                                    <td>



                                                        <telerik:RadTextBox ID="ContactoRepVentaTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox>  


                                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="ContactoRepVentaTel"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> 
--%>                                                    </td>
                                                    <td> </td>
                                                    <td><asp:Label ID="Label72" runat="server" Text="E-mail" /> </td>
                                                    <td>  <telerik:RadTextBox ID="ContactoRepVentaEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox>  </td>
                                                    <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactoRepVentaEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ContactoRepVentaEmail"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> --%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label73" runat="server" Text="Representante de Servicio al Cliente" /> </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoRepServ" runat="server" Width="250px" AutoPostBack="True"
                                                        Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                                
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label74" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoRepServTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox>  
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label77" runat="server" Text="E-mail" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoRepServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox>  
                                                      </td>
                                                    <td> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label78" runat="server" Text="Jefe Servicio a Cliente" />
                                                    </td> <td>
                                                        <telerik:RadComboBox ID="ContactoJefServ" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label79" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoJefServTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox> 
                                                        
                                                    </td>
                                                    <td></td>
                                                    <td> <asp:Label ID="Label80" runat="server" Text="E-mail" /> </td>
                                                    <td> <telerik:RadTextBox ID="ContactoJefServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td>                                                         
                                                    </td>
                                                </tr>                                               
                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label84" runat="server" Text="Asesor de Servicio" /> </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="ContactoAseServ" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>                                                       
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label85" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadTextBox ID="ContactoAseServTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox>                                                         
                                                    </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label86" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoAseServEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td>   
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label87" runat="server" Text="Jefe de Operaciones" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoJefOper" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label88" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadTextBox ID="ContactoJefOperTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox> 
                                                         
                                                     </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label89" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoJefOperEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                    </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="ContactoJefOperEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                           
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label90" runat="server" Text="Coordinador de Almacén Y Rep." /> </td>
                                                    <td> 
                                                           <telerik:RadComboBox ID="ContactoCAlmRep" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                     
                                                     </td>
                                                    <td> </td>
                                                    <td><asp:Label ID="Label91" runat="server" Text="Teléfono" />
                                                    </td><td>
                                                        <telerik:RadTextBox ID="ContactoCAlmRepTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox>
                                                          
                                                    </td>
                                                    <td></td>
                                                    <td>  <asp:Label ID="Label92" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCAlmRepEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                     </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="ContactoCAlmRepEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                                           
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label93" runat="server" Text="Coordinador de Servicio Técnico" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoCServTec" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                                                                    
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label94" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadTextBox ID="ContactoCServTecTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox>  
                                                         
                                                    </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label95" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCServTecEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> 
                                                         
                                                    </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="ContactoCServTecEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>  

                                                    </td>                                                        
                                                </tr>

                                                <tr>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label44" runat="server" Text="Coordinador de Crédito y Cobranza" /> </td>
                                                    <td> 
                                                         <telerik:RadComboBox ID="ContactoCCreCob" runat="server" Width="250px" AutoPostBack="True"
                                                            Filter="Contains" Style="cursor: hand"  OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label46" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadTextBox ID="ContactoCCreCobTelA" runat="server" Width="100px" MaxLength="20"
                                                            MinValue="1">
                                                        </telerik:RadTextBox> 
                                                        
                                                     </td>
                                                    <td>  </td>
                                                    <td>  <asp:Label ID="Label81" runat="server" Text="E-mail" />   </td>
                                                    <td>    <telerik:RadTextBox ID="ContactoCCreCobEmail" runat="server" Width="200px" MaxLength="50">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                        </telerik:RadTextBox> </td>
                                                    <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="ContactoCCreCobEmail"
                                                            Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="Guardar"></asp:RegularExpressionValidator>  
                                                          
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td width="180"></td>
                                                    <td width="257"></td>
                                                    <td width="5"></td>
                                                    <td width="60"></td>
                                                    <td width="100"></td>
                                                    <td width="5"></td>
                                                    <td width="40"></td>
                                                    <td width="100"></td>
                                                    <td></td>
                                                </tr>
                                            </table>                                            
                                            <table width="100%"> 
                                            <thead>  
                                                <tr>                                                      
                                                <th style="font-family:  verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;"   >  CONTACTOS DEL CLIENTE </th>
                                                </tr>  
                                            </thead> 
                                            <tbody>
                                            <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                            </tr>
                                            </tbody>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">   </td>
                                                <td>  </td>
                                                <td>  </td>
                                                    
                                                <td width="10">
                                                </td>
                                                    <td>  </td>
                                                <td>  </td>
                                                   
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">  </td>
                                                <td> <asp:Label ID="Label19" runat="server" Text="Pagos" /></td>
                                                <td><telerik:RadTextBox ID="txtContactoClientePagos" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox></td>
                                                <td>                                                    </td>
                                                <td> <asp:Label ID="Label11" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientePagosTelA" runat="server" Width="100px" MaxLength="20"
                                                        MinValue="1">
                                                    </telerik:RadTextBox>  </td>
                                                <td> </td>
                                                <td><asp:Label ID="Label12" runat="server" Text="E-mail" /> </td>
                                                <td>  <telerik:RadTextBox ID="txtContactoClientePagosEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox>  </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactoClientePagosEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator>  </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label10" runat="server" Text="Compras" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompra" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label14" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompraTelA" runat="server" Width="100px" MaxLength="20"
                                                        MinValue="1">
                                                    </telerik:RadTextBox>  </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label15" runat="server" Text="E-mail" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompraEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox>   </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactoClientecompraEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label13" runat="server" Text="Almacén" />
                                                </td> <td>
                                                    <telerik:RadTextBox ID="txtContactoClientealmacen" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label17" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientealmacenTelA" runat="server" Width="100px" MaxLength="20"
                                                        MinValue="1">
                                                    </telerik:RadTextBox> </td>
                                                <td></td>
                                                <td> <asp:Label ID="Label18" runat="server" Text="E-mail" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientealmacenEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactoClientealmacenEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label16" runat="server" Text="Mantenimiento" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClienteMantenimiento" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td><asp:Label ID="Label20" runat="server" Text="Teléfono" />
                                                </td> <td>
                                                    <telerik:RadTextBox ID="txtContactoClienteMantenimientoTelA" runat="server" Width="100px" MaxLength="20"
                                                        MinValue="1">
                                                    </telerik:RadTextBox></td>
                                                <td></td>
                                                <td> <asp:Label ID="Label21" runat="server" Text="E-mail" /> </td>
                                                <td><telerik:RadTextBox ID="txtContactoClienteMantenimientoEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td> <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtContactoClienteMantenimientoEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator> </td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label22" runat="server" Text="Otros" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClienteOtro" runat="server" Width="257px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>  </td>
                                                <td> </td>
                                                <td>   <asp:Label ID="Label23" runat="server" Text="Teléfono" />
                                                </td> <td>
                                                    <telerik:RadTextBox ID="txtContactoClienteOtroTelA" runat="server" Width="100px" MaxLength="20"
                                                        MinValue="1">
                                                    </telerik:RadTextBox>  </td>
                                                <td>  </td>
                                                <td>  <asp:Label ID="Label24" runat="server" Text="E-mail" />   </td>
                                                <td>    <telerik:RadTextBox ID="txtContactoClienteOtroEmail" runat="server" Width="200px" MaxLength="50">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="Email" />
                                                    </telerik:RadTextBox> </td>
                                                <td>   <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtContactoClienteOtroEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Guardar"></asp:RegularExpressionValidator>  
                                                         
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td width="180"></td>
                                                <td width="257"></td>
                                                <td width="5"></td>
                                                <td width="60"></td>
                                                <td width="100"></td>
                                                <td width="5"></td>
                                                <td width="40"></td>
                                                <td width="100"></td>
                                                <td></td>
                                            </tr>
                                        </table>                                        
  
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>
                        </telerik:radmultipage>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                     <asp:HiddenField ID="HF_ClvPag" runat="server" />
                </td>
            </tr>
        </table>
    </div>


    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function jsFunction() {
                var myselect = document.getElementById("OptGarantia");
                // alert(myselect.options[myselect.selectedIndex].value);

                var opc = myselect.options[myselect.selectedIndex].value;

                switch (opc) {

                    case "-1":
                        $(".divItemsCalendario_rgAcuerdos").hide();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").hide();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").hide();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").hide();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").hide();
                        break;

                    case "0":
                        $(".divItemsCalendario_rgAcuerdos").show();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").hide();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").hide();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").hide();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").hide();
                        break;
                    case "1":
                        $(".divItemsCalendario_rgAcuerdos").hide();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").show();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").hide();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").hide();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").hide();
                        break;
                    case "2":
                        $(".divItemsCalendario_rgAcuerdos").hide();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").hide();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").show();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").hide();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").hide();
                        break;
                    case "3":
                        $(".divItemsCalendario_rgAcuerdos").hide();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").hide();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").hide();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").show();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").hide();
                        break;
                    case "4":
                        $(".divItemsCalendario_rgAcuerdos").hide();
                        $(".divItemsCalendario_rgAcuerdos_Kilo").hide();
                        $(".divItemsCalendario_rgAcuerdos_Comensal").hide();
                        $(".divItemsCalendario_rgAcuerdos_Habitacion").hide();
                        $(".divItemsCalendario_rgAcuerdos_Iguala").show();
                        break;
                }

            }
            function AbrirVentana_Bitacora(Id_Cd, Id_Acys, Pantalla) {
                var oWnd = radopen("Ventana_Bitacora.aspx?Id_Cd=" + Id_Cd
                    + "&Id_Acys=" + Id_Acys
                    + "&Pantalla=" + Pantalla
                    , "AbrirVentana_Bitacora");

                oWnd.setSize(900, 490);
                oWnd.center();
            }


            function allowDrop(e) {
                if (e.preventDefault) {
                    e.preventDefault();
                }
                else
                    e.returnValue = false;

                e.dataTransfer.dropEffect = 'move';

            }




            function drag(e) {
                dragSrcEl = this;
                e.dataTransfer.effectAllowed = 'move';
                var texto = e.srcElement.parentElement.id;
                e.dataTransfer.setData("text", texto);
            }

            function dragEnter(e) {
                // this.classList.add('over');

                if (e.preventDefault) {
                    e.preventDefault();
                }
                else
                    e.returnValue = false;
            }


            function drop(e) {




                if (e.srcElement.className == "divItemsCalendario") {
                    e.returnValue = false;

                    var data = e.dataTransfer.getData("text");

                    if (data.indexOf("div_Cal_Main") != 0) {
                        if (e.srcElement.innerHTML.indexOf("<LABEL>" + data.split("_")[2] + "</LABEL>") < 0)
                            e.srcElement.appendChild(document.getElementById(data));
                    }
                    else {

                        var x = 0;
                        var num = document.getElementById(data).getElementsByTagName("LABEL").length;

                        for (z = 0; z <= num; z++) {
                            if (document.getElementById(data).getElementsByTagName("LABEL")[x]) {

                                if (e.srcElement.innerHTML.indexOf("<LABEL>" + document.getElementById(data).getElementsByTagName("LABEL")[x].innerText + "</LABEL>") < 0) {
                                    e.srcElement.appendChild(document.getElementById(data).getElementsByTagName("LABEL")[x].parentNode);
                                    x = 0;
                                }
                                else {
                                    x++;
                                    z--;
                                }
                            }
                        }

                    }

                }
                else {
                    if (e.srcElement.innerHTML == "") {
                        if (e.preventDefault) {
                            e.preventDefault();
                        }
                        else
                            e.returnValue = false;


                        var data = e.dataTransfer.getData("text");

                        if (data.indexOf("divimg") != 0)
                            if (e.srcElement.tagName != "IMG")
                                e.srcElement.appendChild(document.getElementById(data));
                    }
                }

                LlenaDatosCalendario();
            }


            function LlenaDatosCalendario() {

                document.getElementById("CPH_txtValoresCalendario").value = "";

                var sem = 1;
                for (i = 1; i <= 12; i++) {
                    var nombreCal = "CPH_tblCalendario_" + i.toString();
                    var celdas = document.getElementById(nombreCal).rows[2].cells.length;

                    for (j = 0; j < celdas; j++) {


                        var items = document.getElementById(nombreCal).rows[2].cells[j].getElementsByTagName("LABEL").length;

                        for (k = 0; k < items; k++) {



                            var clase = document.getElementById(nombreCal).rows[2].cells[j].getElementsByTagName("LABEL")[k].parentElement.parentElement.className;
                            var Id_TG = 0;

                            switch (clase) {
                                case "divItemsCalendario_rgAcuerdos":
                                    Id_TG = 0;
                                    break;
                                case "divItemsCalendario_rgAcuerdos_Kilo":
                                    Id_TG = 1;
                                    break;
                                case "divItemsCalendario_rgAcuerdos_Comensal":
                                    Id_TG = 2;
                                    break;
                                case "divItemsCalendario_rgAcuerdos_Habitacion":
                                    Id_TG = 3;
                                    break;
                                case "divItemsCalendario_rgAcuerdos_Iguala":
                                    Id_TG = 4;
                                    break;
                            }

                            document.getElementById("CPH_txtValoresCalendario").value += sem.toString() + "_" + document.getElementById(nombreCal).rows[2].cells[j].getElementsByTagName("LABEL")[k].innerText + "_" + Id_TG.toString() + ",";
                        }
                        sem++;
                    }







                }

            }



            //            function mouseEntra(e) {
            //                // this.classList.add('over');

            //                if (e.preventDefault) {
            //                    e.preventDefault();
            //                }
            //                else
            //                    e.returnValue = false;

            //            }




            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;

                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }


            function pre_validarfecha() {
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                if (txtFecha.get_enabled()) {
                    _ValidarFechaEnPeriodo();
                }
            }
            var txtproductoId;
            var cmbproductoDesc;

            function Id_OnLoad(sender, args) {
                txtproductoId = sender;
            }

            function Desc_OnLoad(sender, args) {
                cmbproductoDesc = sender;
            }
            function txtId_OnBlur(sender, args) {
                OnBlur(sender, cmbproductoDesc);
            }

            function cmbDesc_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtproductoId);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRepresentante.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }

            function txt4_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRutaServicio.ClientID %>'));
            }

            function cmb4_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRutaServicio.ClientID %>'));
            }

            function txt5_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRutaEntrega.ClientID %>'));
            }

            function cmb5_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRutaEntrega.ClientID %>'));
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            function TabSelected(sender, args) {
                var idcte = $find('<%= txtCliente.ClientID %>');
                var idrik = $find('<%= txtRepresentante.ClientID %>');
                var idter = $find('<%= txtTerritorio.ClientID %>');
                var cte = $find('<%= txtClienteNombre.ClientID %>');
                var fol = $find('<%= txtFolio.ClientID %>');
                var rik = $find('<%= cmbRepresentante.ClientID %>');
                //debugger;
                if (idcte.get_value() == "" || idrik.get_value() == "") {
                    var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                    radalert('Favor de seleccionar territorio, representante y cliente</br></br>', 330, 150);
                    radTabStrip.get_allTabs()[1].select();

                    args.set_cancel(true);
                }
                else {
                    var ter_Asesorias = $find('<%= txtTerritorio.ClientID %>');
                    var idrik_Asesorias = $find('<%= txtRepresentante.ClientID %>');
                    var rik_Asesorias = $find('<%= txtRepresentante.ClientID %>');
                    var idcte_Asesorias = $find('<%= txtCliente.ClientID %>');
                    var cte_Asesorias = $find('<%= txtCliente.ClientID %>');
                    var fol_Asesorias = $find('<%= txtFolio.ClientID %>');


                    ter_Asesorias.set_value(idter.get_value());
                    idrik_Asesorias.set_value(idrik.get_value());
                    rik_Asesorias.set_value(rik.get_text());
                    idcte_Asesorias.set_value(idcte.get_value());
                    cte_Asesorias.set_value(cte.get_value());
                    fol_Asesorias.set_value(fol.get_value());


                }
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;

                LlenaDatosCalendario();

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();


                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = false;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }



                switch (button.get_value()) {

                    case 'save':
                        //select tab datos generales
                        LlenaDatosCalendario();
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');




                        if ($find('<%= Fac_Kilo.ClientID %>') != null)
                            if (ValidaNumericObjetoRequerido($find('<%= Fac_Kilo.ClientID %>'), 2) == false) continuarAccion = false

                            if ($find('<%= PNeta_Kilo.ClientID %>') != null)
                                if (ValidaNumericObjetoRequerido($find('<%= PNeta_Kilo.ClientID %>'), 2) == false) continuarAccion = false

                                if ($find('<%= Fac_Comensal.ClientID %>') != null)
                                    if (ValidaNumericObjetoRequerido($find('<%= Fac_Comensal.ClientID %>'), 2) == false) continuarAccion = false

                                    if ($find('<%= PNeta_Comensal.ClientID %>') != null)
                                        if (ValidaNumericObjetoRequerido($find('<%= PNeta_Comensal.ClientID %>'), 2) == false) continuarAccion = false

                                        if ($find('<%= Fac_Habitacion.ClientID %>') != null)
                                            if (ValidaNumericObjetoRequerido($find('<%= Fac_Habitacion.ClientID %>'), 2) == false) continuarAccion = false

                                            if ($find('<%= PNeta_Habitacion.ClientID %>') != null)
                                                if (ValidaNumericObjetoRequerido($find('<%= PNeta_Habitacion.ClientID %>'), 2) == false) continuarAccion = false

                                                if ($find('<%= Fac_Iguala.ClientID %>') != null)
                                                    if (ValidaNumericObjetoRequerido($find('<%= Fac_Iguala.ClientID %>'), 2) == false) continuarAccion = false

                                                    if ($find('<%= PNeta_Iguala.ClientID %>') != null)
                                                        if (ValidaNumericObjetoRequerido($find('<%= PNeta_Iguala.ClientID %>'), 2) == false) continuarAccion = false



                                                        break;


                                                }

                                                //args.set_cancel(!continuarAccion);


                                            }

                                            function validateModalidad() {
                                                var button1 = $find('<%= rdModFrencuenciaEstablecida.ClientID %>');



                                                var button3 = $find('<%= rdModConsignacion.ClientID %>');
                                                //if (!button1.get_checked() || !button2.get_checked() || !button3.get_checked()) {
                                                if (!button1.get_checked() || !button3.get_checked()) {
                                                    radTabStrip.get_allTabs()[1].select();
                                                    return false;
                                                }

                                            }


                                            function ValidaTextboxObjetoRequerido(textBox, indiceTab) {

                                                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');

                                                if (textBox.get_textBoxValue() == '') {
                                                    radTabStrip.get_allTabs()[indiceTab].select();
                                                    return false;
                                                }
                                                return true;
                                            }


                                            function ValidaFechaObjetoRequerido(textBox, indiceTab) {

                                                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');

                                                if (textBox.get_selectedDate() == null) {
                                                    radTabStrip.get_allTabs()[indiceTab].select();
                                                    return false;
                                                }
                                                return true;
                                            }



                                            function ValidaNumericObjetoRequerido(textBox, indiceTab) {


                                                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');

                                                if (textBox.get_value() == '' || textBox.get_value() == '-1') {
                                                    radTabStrip.get_allTabs()[indiceTab].select();
                                                    return false;
                                                }
                                                return true;
                                            }

                                            function popup() {
                                                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                                                oWnd.center();
                                            }
                                            function popup2(Id_Prd, Id_Acs) {
                                                var oWnd = radopen("Ventana_PrdAcys.aspx?Id_Prd=" + Id_Prd + "&Id_Acs=" + Id_Acs, "AbrirVentana_PrdAcys");
                                                oWnd.center();

                                            }

                                            function AbrirVentana_EnviarCorreo(Id_Acs) {
                                                var oWnd = radopen("capAcysEnviarCorreo.aspx?Id_Acs=" + Id_Acs, "AbrirVentana_capAcysEnviarCorreo");
                                                oWnd.setSize(750, 500);
                                                oWnd.center();

                                            }


                                            function AbrirBuscarDireccionEntrega() {
                                                var cte = $find('<%=txtCliente.ClientID%>');
                                                var oWnd = radopen("Ventana_Buscar.aspx?DirEnt=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                                                oWnd.center();
                                            }

                                            function AbrirCalendario(Id_TG) {
                                                var cte = $find('<%=txtCliente.ClientID%>');
                                                var oWnd = radopen("Ventana_Calendario.aspx?Id_TG=" + Id_TG);
                                                oWnd.center();
                                                oWnd.setSize(750, 560);

                                            }


                                            //            function ClienteSeleccionado() {
                                            //                //debugger;
                                            //                var ajaxManager = $find("<%= RAM1.ClientID %>");
                                            //                ajaxManager.ajaxRequest('cliente');
                                            //            }
                                            function ProductosSeleccionados() {
                                                //debugger;
                                                var ajaxManager = $find("<%= RAM1.ClientID %>");
                                                ajaxManager.ajaxRequest('productos');
                                            }


                                            function ClienteSeleccionado(param) {
                                                var ajaxManager = $find("<%= RAM1.ClientID %>");
                                                ajaxManager.ajaxRequest(param);
                                            }




                                            function ObtenerNombre(prd) {
                                                var urlArchivo = 'ObtenerNombre.aspx';
                                                parametros = "prd=" + prd;
                                                parametros = parametros + "&Bi=true";
                                                return obtenerrequest(urlArchivo, parametros);
                                            }
                                            var txtId;
                                            var txtDes;

                                            function ObtenerNombreInicio() {
                                                //debugger;

                                                var actual;

                                                if (txtId.get_value() == '') {
                                                    actual = '';
                                                }
                                                else {
                                                    actual = ObtenerNombre(txtId.get_value());
                                                    if (actual == "-0") {
                                                        window.location.href("Login.aspx");
                                                    }
                                                    else if (actual == "-2") {
                                                        actual = '';
                                                        txtId.set_value(actual);
                                                        AlertaFocus('El producto debe ser un aparato de sistema propietario', txtId._clientID);
                                                        //radalert('El producto debe ser un aparato de sistema propietario', 330, 150);
                                                    }
                                                }
                                                txtDes.set_value(actual);
                                            }
                                            function onLoadIdPrd(sender)
                                            { txtId = sender; }
                                            function onLoadPrdDescr(sender)
                                            { txtDes = sender; }         
        </script>
    </telerik:radcodeblock>
</asp:Content>

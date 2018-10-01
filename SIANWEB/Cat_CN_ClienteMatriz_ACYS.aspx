<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatClienteMatriz_ACYS.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.CatClienteMatriz_ACYS" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
          <AjaxSettings>


            <telerik:AjaxSetting AjaxControlID="rgAcuerdos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

                  <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Kilo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Kilo" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>


             <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Comensal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Comensal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />

                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Habitacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Habitacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                      
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rgAcuerdos_Iguala">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgAcuerdos_Iguala" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>


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
                                    <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                   <%-- <ClientEvents OnDateClick="Calendar_Click" />--%>
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" >
                                     <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdFecha"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Inicio
                            </td>
                            <td>                               
                                <telerik:RadDatePicker ID="rdFechaInicio" runat="server" Width="100px" OnSelectedDateChanged="rdFechaInicioDocumento_SelectedDateChanged"
                                Culture="es-MX" enabled = "True">
                                <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput ID="DateInput2" runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rdFechaInicio"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100px" style="text-align: right;">
                                Fecha Fin
                            </td>
                            <td>
                             <telerik:RadDatePicker ID="rdFechaFin" runat="server" Width="100px" OnSelectedDateChanged="rdFechaFinDocumento_SelectedDateChanged"
                                Culture="es-MX">
                                <Calendar ID="Calendar3" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x" ShowRowHeaders="false">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy" />
                                </Calendar>
                                <DateInput ID="DateInput3" runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"  >
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal"  />
                                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                            </telerik:RadDatePicker><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rdFechaFin"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar" Enabled="false"></asp:RequiredFieldValidator>                           
                            </td>
                            <td width="250">
                            </td>
                            <td width="100px" style="text-align: right; "  visible="false">
                                Estatus
                            </td>
                             <td>
                                <telerik:RadTextBox id="txtEstatus_ACYS" runat="server" enabled="False" width="70px" visible="false">

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
                            <telerik:RadTab runat="server" AccessKey="P" PageViewID="RPVCondicionesPago" Text="4.-Condiciones de &lt;u&gt;p&lt;/u&gt;ago">
                            </telerik:RadTab>                                                     
                            <telerik:RadTab runat="server" AccessKey="S" PageViewID="RPVServicio" Text="5.-&lt;u&gt;S&lt;/u&gt;ervicios de Valor">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="O" PageViewID="RPVOtrosApoyos" Text="6.-<u>O</u>tros Apoyos" >
                            </telerik:RadTab>    
                            
                                 
                        </Tabs>
                    </telerik:RadTabStrip>
                </div>

    


      <telerik:radmultipage id="RadMultiPage1" runat="server" borderstyle="Solid" borderwidth="1px"
                            scrollbars="Auto" selectedindex="0">
                            <%-- Height="415px" Width="880px">--%>

                            <telerik:RadPageView ID="RadPageCliente" runat="server" Width="100%">
                                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="99%" Height="600px"
                                    ResizeMode="AdjacentPane" BorderSize="0" BorderStyle="Solid" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="99%" Height="600px" OnClientResized="onResize"
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
                                              <td></td>
                                                <td><asp:Label ID="Label145" runat="server" Text="Dirección Fiscal: "/></td>
                                                <td>
                                                
                                                      <telerik:RadComboBox ID="cmbDireccionesFiscales" runat="server"  
                                                            DataTextField="ClienteDirFis" DataValueField="Id"  EmptyMessage="Seleccione..."
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" ReadOnly="False"
                                                            MaxHeight="250px" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cmbDireccionesFiscales_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClienteDirFis") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                </td>
                                              </tr>
                                                                                         
                                                <tr> 
                                                    <td>
                                                    </td>
                                                    <td> 
                                                        <asp:Label ID="LabelCliente" runat="server" Text="Cliente"/>
                                                    </td>
                                                    <td width="70" colspan="2">
                                                        <telerik:RadTextBox ID="txtNombre_Cte" runat="server" ReadOnly="true"
                                                            Width="428px">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                          
                                                    <td>
                                                        &nbsp;</td>
                                                    <td Width="20px" >
                                                        Sucursal:
                                                    </td>
                                                    <td>
                                                         <telerik:RadComboBox ID="cmbSucursal" runat="server"  
                                                            DataTextField="Descripcion" DataValueField="Id" 
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" ReadOnly="False"
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
                                                     <td> </td>                                                                                                         
                                                     <td>Segmento:  </td>
                                                     <td>
                                                        <telerik:RadTextBox ID="txtSegmento" runat="server"  width="100" >
                                                        </telerik:RadTextBox> 
                                                      </td>
                                                     <td> </td>
                                                     <td>Formato ACYS:</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtFormatoACYS" runat="server"  width="100" >
                                                        </telerik:RadTextBox> 
                                                    </td>
                                                </tr>
                                                 <tr> 
                                                    <td></td>
                                                    <td> 
                                                        <asp:Label ID="Label59" runat="server" Text="Dirección"/>
                                                    </td>                                                  
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txtDireccion" runat="server" width="220" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label60" runat="server" Text="Colonia"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtColonia" runat="server" ReadOnly="True" width="220" >
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                  
                                                   <td colspan="1">  <asp:Label ID="Label61" runat="server" Text="Municipio"></asp:Label>         </td> 
                                                      <td colspan="6">
                                                        <telerik:RadTextBox ID="txtMunicipio" runat="server" ReadOnly="True" width="410">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                     
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label62" runat="server" Text="Estado"/></td>                                                  
                                                    <td colspan="2">
                                                        <telerik:RadTextBox ID="txtEstado" runat="server" ReadOnly="True" width="185" Text=""  >
                                                        </telerik:RadTextBox>                       </td>                                                                                                                                                              
                                                    <td> <asp:Label ID="Label63" runat="server" Text="C.P." style="text-align:center;"  width="30" ></asp:Label> </td> 
                                                    <td colspan="1">
                                                        <telerik:RadTextBox ID="txtCP" runat="server" ReadOnly="True" width="47">
                                                        </telerik:RadTextBox> </td>
                                                    <td colspan="1">  <asp:Label ID="Label64" runat="server" Text="RFC" width="40" style="text-align:center;" ></asp:Label> </td>
                                                    <td colspan="4">
                                                        <telerik:RadTextBox ID="txtRFC" runat="server" ReadOnly="True" width="175" >
                                                        </telerik:RadTextBox> </td> 
                                                    <td></td>   
                                                    <td> 
                                                        <asp:Label ID="Label65" runat="server" Text="Addenda"></asp:Label>
                                                    </td>
                                                    <td> <asp:CheckBox ID="chkAddenda" runat="server" Text="SI" ReadOnly="False" /> </td>                                                    
                                                    <td> </td>                                                        
                                                     <td>Asignacion de Pedido</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAsignacionDePedido" runat="server"  width="100" >
                                                        </telerik:RadTextBox> 
                                                    </td>                                                  
                                                     <td> </td>                                                    
                                                     <td> </td>  
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="Label106" runat="server" Text="E-mail de recibo de factura electrónica"></asp:Label>
                                                    </td>
                                                    <td colspan="3" >
                                                        <telerik:RadTextBox ID="txtE_mailFacElectronica" runat="server" Width="231px" MaxLength="50" ReadOnly="False">
                                                            <ClientEvents OnFocus="pre_validarfecha" />
                                                        </telerik:RadTextBox>
                                                    
                                                    </td>
                                                     <td colspan="7">&#160; </td>                                                      
                                                     <td> <asp:Label ID="Label9" runat="server" Text="Cuenta Corporativa"></asp:Label> </td>
                                                    <td> <asp:CheckBox ID="chkCuentaCorp" runat="server" Text="SI" ReadOnly="False"/> </td>  
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
                                                        <telerik:RadTextBox ID="txtCom_Nombre" runat="server" Width="275px" MaxLength="250" ReadOnly="False">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                
                                                     <td colspan="7">
                                                        <asp:Label ID="Label67" runat="server" Text="Dirección de Entrega Producto"></asp:Label>
                                                    </td>
                                                    <td>                                                    
                                                            <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarDireccionEntrega_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />

                                                     </td>                                                   
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtCom_DirEntrega" runat="server" ReadOnly="False" Width="400px">
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
                                                        <telerik:RadTextBox ID="txtCom_Colonia" runat="server" ReadOnly="False" width="220">
                                                        </telerik:RadTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td>
                                                        <asp:Label ID="Label69" runat="server" Text="Municipio"></asp:Label>
                                                    </td> 
                                                      <td colspan="7">
                                                        <telerik:RadTextBox ID="txtCom_Municipio" runat="server"  width="220">
                                                        </telerik:RadTextBox>     
                                                       </td>     
                                                        <td></td>
                                                     <td colspan="1">
                                                       <asp:Label ID="Label70" runat="server" Text="Estado" ReadOnly="False"></asp:Label> 
                                                    </td>                                                                                                                                                     
                                                      <td colspan="2">
                                                        <telerik:RadTextBox ID="txtCom_Estado" runat="server" Enabled="False" width="122">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                     <td colspan="1">  <asp:Label ID="Label71" runat="server" Text="C.P."></asp:Label>   </td>   
                                                     <td colspan="1">
                                                        <telerik:RadTextBox ID="txtCom_CP" runat="server" ReadOnly="False" width="60">
                                                        </telerik:RadTextBox>
                                                    </td>  
                                                    <td colspan="1"> <asp:Label ID="Label25" runat="server" Text="No proveedor" /> </td>
                                                    <td colspan="2"> <telerik:RadTextBox ID="txtCom_NoProv" runat="server" Width="70px" MaxLength="9" ReadOnly="False">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox> </td>                                                                                                                                                                                                                                                    
                                                </tr>
                                                <tr> 
                                                    <td> </td>
                                                     <td>  <asp:Label ID="Label3" runat="server" Text="Contacto principal" /> </td>
                                                    <td  colspan="4">
                                                        <telerik:RadTextBox ID="txtCom_Contacto" runat="server" Width="275px" MaxLength="100" ReadOnly="False">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>                                                    
                                                    <td>   <asp:Label ID="Label4" runat="server" Text="Puesto" />  </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtCom_Puesto" runat="server" Width="175px" MaxLength="50" ReadOnly="False">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        
                                                    </td>                                                    
                                                     <td >
                                                        <asp:Label ID="Label6" runat="server" Text="Teléfono" />
                                                    </td>
                                                    <td colspan="2" >
                                                        <telerik:RadTextBox ID="txtCom_Telefono" runat="server"  Width="122px"  MaxLength="9" ReadOnly="False">                                                         
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
                                                        <telerik:RadNumericTextBox ID="txtCom_Id_Territorio" runat="server" MaxLength="9" MinValue="1"
                                                            Width="70px" ReadOnly="False">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                     
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbCom_Territorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            MaxHeight="250px" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbTer_SelectedIndexChanged" Width="370px" ReadOnly="False">
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCom_Id_Territorio"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"  Enabled="false" ></asp:RequiredFieldValidator>
                                                    </td>   
                                                     <td  Width="20px" ></td>                                                   
                                                    <td  >   </td> 
                                                    <td colspan="1" >   
                                                        <asp:Label ID="Label1" runat="server" Text="Ruta de servicio"></asp:Label> 
                                                    </td>
                                                    <td  colspan="1">
                                                        <telerik:RadNumericTextBox ID="txtCom_IdRutaServicio" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="False">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt4_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                     <td  colspan="6">
                                                        <telerik:RadComboBox ID="cmbCom_RutaServicio" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb4_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="False">
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
                                                        <telerik:RadNumericTextBox ID="txtCom_IdRepresentanteVentas" runat="server" MinValue="1" Width="70px"
                                                            MaxLength="9" ReadOnly="False">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>                                                                                                                                                              
                                                    <td colspan="8">
                                                        <telerik:RadComboBox ID="cmbCom_RepresentanteVentas" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccione un territorio"
                                                            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbRepresentante_SelectedIndexChanged" Width="370px" ReadOnly="False"
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCom_IdRepresentanteVentas"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar" Enabled="false"></asp:RequiredFieldValidator>
                                                    </td>  
                                                    <td></td>    
                                                    <td></td> 
                                                    <td><asp:Label ID="Label2" runat="server" Text="Ruta de entrega"></asp:Label> </td>
                                                    <td colspan="1">  <telerik:RadNumericTextBox ID="txtCom_RutaEntregaa" runat="server" Width="70px" MinValue="1"
                                                            MaxLength="9" ReadOnly="False">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnBlur="txt5_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>                  </td>
                                                    <td colspan="6">
                                                        <telerik:RadComboBox ID="cmbCom_IdRutaEntrega" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true" 
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmb5_ClientSelectedIndexChanged"
                                                            Width="340px" OnClientFocus="pre_validarfecha" MaxHeight="250px" ReadOnly="False">
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
                                                <td></td>
                                                    <td><asp:CheckBox runat="server" ID="chkGarantia" Text="Garantía" AutoPostBack="true"/></td> 
                                                    <td><asp:CheckBox runat="server" ID="chkServicios" Text="Servicios" AutoPostBack="true"/></td>
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
                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="100%" ResizeMode="AdjacentPane" Height="550px"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane2" runat="server" OnClientResized="onResize" Height="550px">
                                    <div runat="server" id="div2" style="font-family: verdana; font-size: 8pt">                                    
                                        <table style="border:1px solid black; margin-right:0" width="100%" >                                                                                                                                 
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
                                                <td colspan="4">
                                                 <asp:RadioButton ID="rdModOrdenAbierta" runat="server" Text="Orden Abierta con Reposición / Release"  GroupName="ModalidadPedido" OnCheckedChanged="RbModalidad_CheckedChanged" AutoPostBack="True"/>
                                                </td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModConsignacion"  runat="server" Text="Consignación" GroupName="ModalidadPedido" Enabled="false" />
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
                                                    <td> <asp:CheckBox ID="chkEmail" runat="server" Text="Email" />  </td>
                                                     <td width="100px"> </td>
                                                    <td ><asp:CheckBox ID="chkFax" runat="server" Text="Fax"/> </td>
                                                    <td><asp:CheckBox ID="chkTelefono" runat="server" Text="Teléfono"/> </td>    
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
                                                        <telerik:RadTextBox ID="txtPedidoPuesto" runat="server" Width="245px" MaxLength="250">
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtpedidoPuesto"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator> --%>
                                                     </td>                                                                                                                                                
                                                     <td colspan="1">
                                                        <asp:Label ID="LabelPedidoTelefono" runat="server" Text="Teléfono:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="2">
                                                    <telerik:RadTextBox ID="txtPedidoTelefono" runat="server" Width="180px">
                                                        </telerik:RadTextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtpedidotelefono"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>--%>
                                                    </td>  
                                                     <td colspan="1"  width="50px">
                                                        <asp:Label ID="Label96" runat="server" Text="Email:"></asp:Label>
                                                    </td>                                                    
                                                    <td colspan="1" Width="264px">
                                                        <telerik:RadTextBox ID="txtPedidoEmail" runat="server"  Width="257px">
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
                                                  <td>Documentación requerida para entrega  </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkRecDocOrdenCompra" runat="server" Text="Orden de compra / Release" />
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                         <asp:CheckBox ID="chkRecDocReposicion" runat="server" Text="Orden de Reposición" />
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                       <asp:CheckBox ID="ChkRecDocFolio" runat="server" Text="Folio" />
                                                    </td>                                                    
                                                </tr>
                                                <tr> 
                                                    <td></td>
                                                    <td><asp:Label ID="Label55" runat="server" Text="Otro:"/></td>
                                                    <td colspan="7"  >
                                                        <telerik:RadTextBox ID="txtRecDocOtro" runat="server" ReadOnly="False" width="600">
                                                        </telerik:RadTextBox> 
                                                     </td>                                                                                                                                                                                                                                                                                                                                    
                                                </tr>
                                                 <tr>
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
                                                </tr>  
                                                 <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>                                                    
                                                </tr>                                                                                             
                                            </table>                                                                                 
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
                                                        <td></td>
                                                        <td>
                                                       
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                       
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
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

                                      <table width="100%" style="border:1px solid black; margin-right:0">
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
                                                                <asp:Label ID="Label7" runat="server" Text="Lunes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label8" runat="server" Text="Martes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label82" runat="server" Text="Miércoles"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label83" runat="server" Text="Jueves"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label99" runat="server" Text="Viernes"></asp:Label>
                                                            </td>
                                                            <td width="50" align="center">
                                                                <asp:Label ID="Label101" runat="server" Text="Sábado"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">
                                                            </td>
                                                            <td height="10" width="100">
                                                                <asp:Label ID="Label104" runat="server" Text="Días de revisión"></asp:Label>
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
                                                                <asp:Label ID="Label105" runat="server" Text="Horarios de revisión"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Culture="es-MX"
                                                                    Width="100px">
                                                                    <Calendar ID="Calendar14" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView9" runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="cabezera">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput14" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar15" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView10" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput15" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                y
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker3" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar16" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView11" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput16" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                            <td>
                                                                a
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="RadTimePicker4" runat="server" Width="100px">
                                                                    <Calendar ID="Calendar17" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                                    <TimeView ID="TimeView12" runat="server" CellSpacing="-1" Culture="es-MX">
                                                                    </TimeView>
                                                                    <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios." />
                                                                    <DateInput ID="DateInput17" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                        LabelCssClass="" Width="">
                                                                    </DateInput></telerik:RadTimePicker>
                                                            </td>
                                                        </tr>
                                                        <tr> 
                                                         <td width="10">
                                                            </td>                                                           
                                                             <td valign="top" width="100">
                                                                <asp:Label ID="Label109" runat="server" Text="Persona que recibe: "></asp:Label>
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

                                                                        <telerik:RadNumericTextBox ID="txtRecCitaDiasdeAnticipacion" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
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
                                                                    <telerik:RadNumericTextBox ID="txtRecEstMonto" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
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
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactFranquiciaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactFranquiciaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label119" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactKeyEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFactKeyRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label120" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdCompraEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdCompraRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label121" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdReposEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocOrdReposRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label122" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocCopPedidoEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocCopPedidoRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label123" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocRemisionEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocRemisionRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label124" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFolioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocFolioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label125" runat="server" Text="Contra recibo" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocContraRecEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocContraRecRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label127" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocEntAlmacenEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocEntAlmacenRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label128" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocSopServicioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocSopServicioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label129" runat="server" Text="Nombre y Firma de recibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkRecDocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocNomFirmaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecDocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecDocNomFirmaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label126" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkRecCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecCitaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkRecCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtRecCitaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
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
                                <telerik:RadSplitter ID="RadSplitter4" runat="server" Width="100.5%" ResizeMode="AdjacentPane" Height="700px"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane4" runat="server" Width="100%" OnClientResized="onResize" Height="700px">                                     
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
                                                            <Calendar ID="Calendar12" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                ViewSelectorText="x" ShowRowHeaders="false">
                                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                    TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput ID="DateInput12" runat="server" AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
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
                                                            <Calendar ID="Calendar13" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                            </Calendar>
                                                            <DateInput ID="DateInput13" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
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
                                                         <thead >
                                                        <tr>
                                                        <th  style="font-family:  verdana; font-size: 10pt; border:1px solid black;  border-collapse:collapse;" colspan="21"    >  3.1 ACUERDO ECONOMICO DE PRODUCTO</th>                                                         
                                                        </tr>
                                                        </thead>
                                                    <tr>
                                                        <td style="width:200px"><b>FACTURADO</b></td> <td style="width:100px">&nbsp;</td>
                                                    </tr>
                                                   </table>

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
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
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
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server" Text='<%# Bind("Unidad") %>'  />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server"  Text='<%# Bind("Unidad") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Precio vta." UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
                                                                            Width="50px" MinValue="1" MaxLength="9" Enabled="false">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="L">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false"/>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" Enabled="false" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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

                                                  <div id="divServicios" runat="server">  
                                              <br />
                                                <table width="100%">
                                                                                                       <thead >
                                                        <tr>
                                                        <th  style="font-family:  verdana; font-size: 10pt; border:1px solid black;  border-collapse:collapse;" colspan="21"    >  3.2 ACUERDO ECONOMICO DE SERVICIOS</th>                                                         
                                                        </tr>
                                                        </thead>
                                                    <tr>
                                                        <td style="width:200px"><b>SERVICIOS</b></td> <td style="width:100px">&nbsp;</td>

                                                     
                                                        <td></td>
                                                    </tr>
                                                </table>

                                                 <telerik:RadGrid ID="rgAcuerdos_Servicios" runat="server" AllowPaging="False" AutoGenerateColumns="False"
 							                            GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        BorderStyle="None" ShowFooter="true" 
                                                        OnNeedDataSource="rgAcuerdos_Servicios_NeedDataSource" PageSize="6" OnItemCommand="rgAcuerdos_Servicios_ItemCommand"
                                                        OnItemDataBound="rgAcuerdos_Servicios_ItemDataBound" OnItemCreated="rgAcuerdos_Servicios_ItemCreated"
                                                         OnPreRender="rgAcuerdos_Servicios_PreRender">

                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id_Prd">
                                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
	                                                                    MinValue="1" MaxLength="9" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
																				Width="100px">
                                                                            <ClientEvents OnLoad="Id_OnLoad" />
	                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Descripción del servicio" 
                                                                        UniqueName="Prd_Descripcion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant. de servicios" UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Costo de servicio" UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
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
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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

                                                <div id="divGarantias" runat="server">

                                                     <div id="div_Kilo" runat="server">
                                                <br />
                                                <table width="100%">
                                                       <thead >
                                                        <tr>
                                                        <th  style="font-family:  verdana; font-size: 10pt; border:1px solid black;  border-collapse:collapse;" colspan="21"    >  3.3 ACUERDO ECONOMICO DE GARANTIAS</th>                                                         
                                                        </tr>
                                                        </thead>
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
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
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
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server" Text='<%# Bind("Unidad") %>'  />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server"  Text='<%# Bind("Unidad") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Precio vta." UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
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
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
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
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server" Text='<%# Bind("Unidad") %>'  />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server"  Text='<%# Bind("Unidad") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Precio vta." UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
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
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
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
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server" Text='<%# Bind("Unidad") %>'  />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server"  Text='<%# Bind("Unidad") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Precio vta." UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
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
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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
                                                                            <asp:Label ID="lblId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" DbValue='<%# Bind("Id_Prd" ) %>'  
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
                                                                        <asp:Label ID="txtDescripcion" runat="server" Width="170px" Text='<%# Bind("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="170px" Text='<%# Bind("Descripcion") %>'>
                                                                        </telerik:RadTextBox></EditItemTemplate>
                                                                    <HeaderStyle Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtPresentacion" runat="server"  Text='<%# Bind("Presentacion") %>'/>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Prd_UniNom" HeaderText="Uni." UniqueName="Prd_UniNom">
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server" Text='<%# Bind("Unidad") %>'  />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="txtUnidad" runat="server"  Text='<%# Bind("Unidad") %>' />
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acys_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                                    <HeaderStyle Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad" ) %>' 
                                                                            Width="40px" MinValue="1" MaxLength="9">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Precio vta." UniqueName="Precio">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="dblPrecio" runat="server" Text='<%# Bind("Precio","{0:N2}") %>' 
                                                                            Width="50px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>       

                                                              <telerik:GridTemplateColumn DataField="Precio"  HeaderText="Subtotal" UniqueName="Subtotal">
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblSubtotal" runat="server"  Text='<%# Eval("Precio") == DBNull.Value ? "":CalculaSubtotal(Convert.ToDouble(Eval("Cantidad")),Convert.ToDouble(Eval("Precio"))).ToString("N2")%>' ></asp:Label>
                                                                     </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblPiePrecio" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                               </telerik:GridTemplateColumn> 

                                                                <telerik:GridTemplateColumn DataField="Frecuencia" HeaderText="Frec. semana(s)"
                                                                    UniqueName="Frecuencia" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server"  Text='<%# Bind("Frecuencia") %>' 
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
                                                                        <asp:RadioButton ID="chkLunL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkLun" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Lun") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Lun")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMarL" runat="server"  Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMar" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mar") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mar")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="M">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkMieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkMie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Mie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="J">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkJueL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkJue" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Jue") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Jue")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="V">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkVieL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            Enabled="false" /></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkVie" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Vie") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Vie")) %>'
                                                                            GroupName="DiaEntrega" />
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="35px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="S">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="chkSabL" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:RadioButton ID="chkSab" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Sab") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sab")) %>'
                                                                            GroupName="DiaEntrega" /></EditItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumento" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Documento").ToString() == "F" ? "Factura" : "Remision" %>'></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadComboBox ID="chkDocumento" runat="server" 
                                                                                Width="80px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                                    <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                                                </Items>
                                                                          </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                 <telerik:GridTemplateColumn DataField="Acys_cantTotal" HeaderText="Cantidad Total"
                                                                    UniqueName="Acys_cantTotal"  Display="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label98" runat="server" ></asp:Label></ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadNumericTextBox ID="txtCantTotal" runat="server" 
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

                                                </div>




  



                                                </td>
                                            </tr>
                                        </table>
                                          </div>                                       
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>

                            <telerik:RadPageView ID="RPVCondicionesPago" runat="server" Enabled="True">
                                <telerik:RadSplitter ID="RadSplitter5" BorderSize="0" runat="server" Width="99%" Height="600px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane5" runat="server" Width="99%" Height="600px" OnClientResized="onResize">
                                    
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
                                                    <asp:CheckBox ID="chkCredito" runat="server" Text="Crédito" Enabled=true />
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
                                                    <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.1 Formas de Pago</th>                                                         
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
                                                        <asp:CheckBox ID="chkDeposito" runat="server" Text="Deposito" />
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
                                                        <asp:CheckBox ID="chkFactoraje" runat="server" Text="Factoraje" />
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
                                                        <asp:CheckBox ID="chkTransferencia" runat="server" Text="Transferencia" />
                                                    </td>
                                                    <td>
                                                        &#160;                                                            
                                                    </td>
                                                        <td>
                                                        <asp:CheckBox ID="chkTarjetaCredito" runat="server" Text="Tarjeta de Crédito" />
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
                                                        <asp:CheckBox ID="chkCheque" runat="server" Text="Cheque" />
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
                                            <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.2 Revisión de Facturas</th>                                                         
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
                                                                    <Calendar ID="Calendar5" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
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
                                                        <th  style="font-family: verdana; font-size: 10pt; border:1px solid black; border-collapse:collapse;" colspan="21"  >  4.3 Pago de Facturas</th>                                                         
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
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactFranquiciaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactFranquiciaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label196" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkCPFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactKeyEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFactKeyRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label197" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkCPOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdCompraEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdCompraRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label198" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkCPOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdReposEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPOrdReposRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label199" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkCPCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPCopPedidoEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPCopPedidoRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label200" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkCPRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRemisionEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRemisionRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label201" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkCPFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFolioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPFolioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label202" runat="server" Text="Contra recibo" /></td>
                                                            <td><asp:CheckBox ID="chkCPContraRecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPContraRecEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPContraRecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPContraRecRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label203" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkCPEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPEntAlmacenEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPEntAlmacenRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label204" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkCPSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPSopServicioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPSopServicioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label205" runat="server" Text="Nombre y Firma de recibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkCPNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPNomFirmaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPNomFirmaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label206" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkCPRecCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRecCitaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkCPRecCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtCPRecCitaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
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
                                        <table>
                                            <tr>
                                                <td width="10"></td>
                                                <td width="50">&nbsp;&nbsp;</td>
                                                <td valign="middle"></td>
                                                <td width="10"></td>
                                                <td width="50"></td>
                                                <td></td>
                                                <td width="70"></td>
                                                <td width="70"></td>
                                            </tr>
                                            <tr>
                                                
                                                <td colspan="5">
                                                     <asp:Label ID="Label48" runat="server" Text="5.1 Visita del Representante" Font-Bold="true"></asp:Label>    
                                                 </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                             <td>
                                                Frecuencia
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtFrecuncia" runat="server" Width="50px" MaxLength="9"
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
                                             <table>
                                            <tr>
                                               <td colspan="5">
                                                <asp:Label ID="Label50" runat="server" Text="5.2 Servicio de Asesoria" Font-Bold="true"></asp:Label>    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>                                                            
                                                <td>
                                                    <asp:CheckBox ID="chkServAsesoria" runat="server"  OnCheckedChanged="ChkServAsesoria_CheckedChanged" Text="Requiere Servicio de Asesoria" 
                                                        Checked="True" />
                                                </td>                                                            
                                             </tr>
                                             <tr>
                                                <td width="10"></td>                                           
                                              </tr>
                                                <tr id="AsesoriaListado" runat="server">
                                                    <td width="10">  </td>
                                                      <td></td>
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
                                                            <td><asp:CheckBox ID="chkServAseDocFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFactFranquiciaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFactFranquiciaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label152" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFactKeyEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFactKeyRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label153" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocOrdCompraEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocOrdCompraRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label154" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocOrdReposEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocOrdReposRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label155" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocCopPedidoEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocCopPedidoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocCopPedidoRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label156" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocRemisionEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocRemisionRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label157" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFolioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocFolioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label158" runat="server" Text="Contra ServAseibo" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocContraServAseEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocContraServAseEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocContraServAseRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocContraServAseRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label159" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocEntAlmacenEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocEntAlmacenRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>


                                                <tr>
                                                            <td><asp:Label ID="Label160" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocSopServicioEnt" runat="server" /></td>
                                                          <td>    <telerik:RadNumericTextBox ID="txtServAseDocSopServicioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                           </td>
                                                           <td><asp:CheckBox ID="chkServAseDocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocSopServicioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                            </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label161" runat="server" Text="Nombre y Firma de ServAseibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkServAseDocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocNomFirmaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseDocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseDocNomFirmaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label162" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkServAseCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseCitaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServAseCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServAseCitaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
    
                                                </tr>
                                            </table>
                                                    
                                                    
                                                    </td>
                                                 </tr>                                              
                                        </table>
                                        <table width="95%">    <%--INICIO--%>
                                        <tr>
                                           <td colspan="5">  
                                            <asp:Label ID="Label49" runat="server" Text="5.3 Servicio Técnico" Font-Bold="true"></asp:Label>
                                           </td>
                                        </tr>                                         
                                         <tr>
                                            <td width="10"></td>
                                            <td>    <asp:Label ID="Label103" runat="server" Text="A) Equipos de Servicio (Relleno)"></asp:Label>    </td>
                                          </tr>
                                          <tr>
                                            <td>
                                            </td>                                                            
                                            <td>
                                                <asp:CheckBox ID="chkServTecnicoRelleno" runat="server" Text="Requiere Servicio a equipos(Relleno) " OnCheckedChanged="ChkServTecnicoRelleno_CheckedChanged"  Checked="True" />
                                            </td>                                                            
                                         </tr>                                            
                                            <tr id="EquipoRellenoListado" runat="Server">
                                                <td width="10">       </td>
                                                <td>
                                        
                                    
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
                                            <td> <asp:CheckBox ID="chkServMantenimiento" runat="server" Text="Requiere Servicio Mantenimiento Preventivo/Revisión"   OnCheckedChanged="ChkServMantenimiento_CheckedChanged"  Checked="True" /></td>
                                        </tr>                                              
                                         <tr id="MantenimientoPreventivoListado" runat="Server">
                                                <td width="10"></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td><asp:Label ID="Label163" runat="server" Text="Documentos" /></td>
                                                            <td><asp:Label ID="Label164" runat="server" Text="Entrega" /></td>
                                                            <td><asp:Label ID="Label165" runat="server" Text="No. Copias" /></td>
                                                            <td><asp:Label ID="Label166" runat="server" Text="ServTecepción" /></td>
                                                            <td><asp:Label ID="Label167" runat="server" Text="No.Copias" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label168" runat="server" Text="Factura Franquicia" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocFactFranquiciaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFactFranquiciaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocFactFranquiciaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFactFranquiciaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label169" runat="server" Text="Factura Key" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocFactKeyEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFactKeyEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocFactKeyRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFactKeyRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label170" runat="server" Text="Orden de Compra/Release" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocOrdCompraEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocOrdCompraEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocOrdCompraRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocOrdCompraRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label171" runat="server" Text="Orden de reposición" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocOrdReposEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocOrdReposEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocOrdReposRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocOrdReposRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label172" runat="server" Text="Copia de pedido" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocCopPedidoEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocCopPedidoEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocCopPedidoServTec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocCopPedidoServTecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                      <tr>
                                                            <td><asp:Label ID="Label173" runat="server" Text="Remisión" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocRemisionEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocRemisionEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocRemisionRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocRemisionRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                                                                                <tr>
                                                            <td><asp:Label ID="Label174" runat="server" Text="Folio" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocFolioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFolioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocFolioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocFolioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                        </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label175" runat="server" Text="Contra ServTecibo" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocContraServTecEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocContraServTecEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocContraServTecRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocContraServTecRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>

                                                    </td>
                                                </tr>

                                                <tr>
                                                            <td><asp:Label ID="Label176" runat="server" Text="Entrada al almacen" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocEntAlmacenEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocEntAlmacenEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocEntAlmacenRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocEntAlmacenRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>

                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label177" runat="server" Text="Soporte de servicio" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocSopServicioEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocSopServicioEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocSopServicioRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocSopServicioRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
 
                                                </tr>
                                                <tr>
                                                            <td><asp:Label ID="Label178" runat="server" Text="Nombre y Firma de ServTecibido en documento" /></td>
                                                            <td><asp:CheckBox ID="chkServTecDocNomFirmaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocNomFirmaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecDocNomFirmaoRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecDocNomFirmaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
           
                                                </tr>

                                             <tr>
                                                            <td><asp:Label ID="Label179" runat="server" Text="Cita" /></td>
                                                            <td><asp:CheckBox ID="chkServTecCitaEnt" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecCitaEntCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
                                                           <td><asp:CheckBox ID="chkServTecCitaRec" runat="server" /></td>
                                                            <td>    <telerik:RadNumericTextBox ID="txtServTecCitaRecCop" runat="server" MaxLength="9" MinValue="1"
                                                                         Width="70px" >
                                                                         <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                         <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                             </td>
    
                                                </tr>
                                            </table>
                                                </td>                                         
                                            </tr>
                                             <tr>
                                                <td>&#160;</td>
                                            </tr>
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
                                    BorderSize="0" ResizeWithBrowserWindow="true" Height="615px">
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
                                                        <telerik:RadComboBox ID="ContactoRepVenta" runat="server" Width="250px" 
                                                        Filter="Contains" Style="cursor: hand" 
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                     <%--   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" ControlToValidate="ContactoRepVenta"
                                                            InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                                            Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />  --%>                                                  
                                                     </td>
                                                    <td></td>
                                                    <td> <asp:Label ID="Label66" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoRepVentaTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
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
                                                        <telerik:RadComboBox ID="ContactoRepServ" runat="server" Width="250px" 
                                                        Filter="Contains" Style="cursor: hand" 
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                                
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label74" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoRepServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>  
                                                       
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
                                                        <telerik:RadComboBox ID="ContactoJefServ" runat="server" Width="250px" 
                                                            Filter="Contains" Style="cursor: hand" 
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td> <asp:Label ID="Label79" runat="server" Text="Teléfono" /> </td>
                                                    <td> <telerik:RadNumericTextBox ID="ContactoJefServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                        
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
                                                        <telerik:RadComboBox ID="ContactoAseServ" runat="server" Width="250px" 
                                                            Filter="Contains" Style="cursor: hand" 
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>                                                       
                                                       
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label85" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoAseServTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>                                                         
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
                                                         <telerik:RadComboBox ID="ContactoJefOper" runat="server" Width="250px" 
                                                            Filter="Contains" Style="cursor: hand" 
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                    </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label88" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoJefOperTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                         
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
                                                           <telerik:RadComboBox ID="ContactoCAlmRep" runat="server" Width="250px"
                                                            Filter="Contains" Style="cursor: hand"
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                     
                                                     </td>
                                                    <td> </td>
                                                    <td><asp:Label ID="Label91" runat="server" Text="Teléfono" />
                                                    </td><td>
                                                        <telerik:RadNumericTextBox ID="ContactoCAlmRepTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                          
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
                                                            Filter="Contains" Style="cursor: hand" 
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                                                                    
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label94" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoCServTecTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>  
                                                         
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
                                                         <telerik:RadComboBox ID="ContactoCCreCob" runat="server" Width="250px" 
                                                            Filter="Contains" Style="cursor: hand" 
                                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                                        </telerik:RadComboBox>
                                                        
                                                     </td>
                                                    <td> </td>
                                                    <td>   <asp:Label ID="Label46" runat="server" Text="Teléfono" />
                                                    </td> <td>
                                                        <telerik:RadNumericTextBox ID="ContactoCCreCobTel" runat="server" Width="100px" MaxLength="9"
                                                            MinValue="1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox> 
                                                        
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
                                                <td> <telerik:RadTextBox ID="txtContactoClientePagosTel" runat="server" Width="100px">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
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
                                                <td> <telerik:RadTextBox ID="txtContactoClientecompraTel" runat="server" Width="100px" >
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
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
                                                <td> <telerik:RadTextBox ID="txtContactoClientealmacenTel" runat="server" Width="100px">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
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
                                                    <telerik:RadTextBox ID="txtContactoClienteMantenimientoTel" runat="server" Width="100px">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
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
                                                    <telerik:RadTextBox ID="txtContactoClienteOtroTel" runat="server" Width="100px">
                                                        <ClientEvents OnFocus="pre_validarfecha" OnKeyPress="handleClickEvent" />
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




                            <telerik:RadPageView ID="RPV_SIANCENTRAL" runat="server" Width="100%">
                                 <telerik:RadSplitter ID="RadSplitter3" runat="server" Width="100%" ResizeMode="AdjacentPane"
                                    BorderSize="0" ResizeWithBrowserWindow="true">
                                    <telerik:RadPane ID="RadPane3" runat="server" OnClientResized="onResize">
                                        
                                         <table style="border:1px solid black; margin-right:0" width="100%" >                                                                                                                                 
                                              <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > 8.- DATOS MACCOLA PARA LA MATRIZ</th>                                                         
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
                                                <td>No. Cliente</td>
                                                <td>
                                                      <telerik:RadNumericTextBox ID="txtNumClienteMac" runat="server" Width="50px">
                                                        </telerik:RadNumericTextBox>
                                                </td>

                                                 <td>Nombre</td>
                                                <td>
                                                      <telerik:RadTextBox ID="txtNombreClienteMac" runat="server" Width="300px">
                                                        </telerik:RadTextBox>
                                                </td>
                                                    <td>Condiciones </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtCondicionesClienteMac" runat="server" Width="50px">
                                                        </telerik:RadTextBox>
                                                   </td>

                                                   
                                              </tr>
                                              <tr>
                                                  <td>
                                              
                                                  </td>
                                              </tr>
                                              <tr>
                                                <td>Datos de Facturación</td>
                                              </tr>
                                              <tr>
                                                    <td>Calle</td>
                                                    <td> <telerik:RadTextBox ID="txtCalleClienteMac" runat="server" Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>Ciudad</td>
                                                    <td>
                                                         <telerik:RadTextBox ID="txtCiudadClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Zona Postal
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtZonaPostalClienteMac" runat="server" Width="100px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                              </tr>
                                              <tr>
                                                <td>País</td>
                                                <td>    
                                                        <telerik:RadTextBox ID="txtPaisClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox> 
                                                </td>
                                                <td>
                                                    Estado
                                                </td>
                                                <td>
                                                       <telerik:RadTextBox ID="txtEstadoClienteMac" runat="server" Width="150px">
                                                        </telerik:RadTextBox> 
                                                </td>
                                              </tr>
                                              <tr>
                                                <td colspan="4"></td>

                                                <td>Tipo</td>
                                                <td>    <telerik:RadNumericTextBox ID="txtTipoClientMac" runat="server" Width="50px">
                                                        </telerik:RadNumericTextBox> 
                                                 </td>
                                              </tr>
                                              <tr>
                                                    <td>Territorio</td>
                                                    <td> 
                                                        <telerik:RadNumericTextBox ID="txtTerritorioClienteMac" runat="server" Width="150px">
                                                        </telerik:RadNumericTextBox> 
                                                     </td>
                                              </tr>
                                              <tr>
                                                    <td>Vendedor</td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtVendedorClienteMac" runat="server" Width="150px">
                                                        </telerik:RadNumericTextBox> 
                                                    </td>
                                                    <td>
                                                        Tarifa de Impuesto
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtTarifaImpClienteMac" runat="server" Width="150px">
                                                        </telerik:RadNumericTextBox> 
                                                    </td>
                                              </tr>
                                               <tr>
                                                 <td></td>
                                              </tr> 
                                              <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     <asp:CheckBox ID="chkGravableClienteMac" runat="server" Text="Gravable" />
                                                </td>
                                              
                                              </tr>
                                               <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     <asp:CheckBox ID="chkPagoComisionClienteMac" runat="server" Text="Pago Comisión" />
                                                </td>
                                         
                                              </tr>
                                              <tr>
                                                <td colspan="2"></td>
                                                <td> 
                                                     <asp:CheckBox ID="chkActCostoGarantíaClienteMac" runat="server" Text="Activar Costo Garantía" />
                                                </td>
                                              
                                              </tr>
                                                
                                        </table>
                                        <br />

                                        <table style="border:1px solid black; margin-right:0" width="100%" >                                                                                                                                 
                                              <thead>
                                                <tr>
                                                    <th  style="font-family:verdana; font-size: 10pt;border:1px solid black;  border-collapse:collapse;" colspan="9"    > DIRECCIONES FISCALES ADICIONALES DEL CLIENTE</th>                                                         
                                                </tr>                                                        
                                              </thead>

                                              <telerik:RadGrid ID="rgDirFiscales" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        BorderStyle="None" ShowFooter="true"
                                                        OnNeedDataSource="rgDirFiscales_NeedDataSource" OnItemCommand="rgDirFiscales_ItemCommand"
                                                         >
                                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="Id">
                                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                         <Columns>


                                                              <telerik:GridTemplateColumn DataField="ClienteDirFis" HeaderText="Cliente." UniqueName="ClienteDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblCliente" runat="server" Text='<%# Bind("ClienteDirFis") %>'></asp:Label> 
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtClienteDirFis" runat="server" Text='<%# Bind("ClienteDirFis") %>'
	                                                                   MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox>
                                                                    
                                                                   </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn DataField="DireccionDirFis" HeaderText="Dirección." UniqueName="DireccionDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("DireccionDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtDireccionDirFis" runat="server" Text='<%# Bind("DireccionDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                             <telerik:GridTemplateColumn DataField="EstadoDirFis" HeaderText="Estado." UniqueName="EstadoDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblEstadoDirFis" runat="server" Text='<%# Bind("EstadoDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtEstadoDirFis" runat="server" Text='<%# Bind("EstadoDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                            <telerik:GridTemplateColumn DataField="ColoniaDirFis" HeaderText="Colonia." UniqueName="ColoniaDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblColoniaDirFis" runat="server" Text='<%# Bind("ColoniaDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtColoniaDirFis" runat="server" Text='<%# Bind("ColoniaDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn DataField="CPDirFis" HeaderText="CP." UniqueName="CPDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblCPDirFis" runat="server" Text='<%# Bind("CPDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtCPDirFis" runat="server" Text='<%# Bind("CPDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


                                                          <telerik:GridTemplateColumn DataField="MunicipioDirFis" HeaderText="Municipio" UniqueName="MunicipioDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblMunicipioDirFis" runat="server" Text='<%# Bind("MunicipioDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtMunicipioDirFis" runat="server" Text='<%# Bind("MunicipioDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>



                                                            <telerik:GridTemplateColumn DataField="RFCDirFis" HeaderText="RFC" UniqueName="RFCDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblRFCDirFis" runat="server" Text='<%# Bind("RFCDirFis") %>'></asp:Label> </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtRFCDirFis" runat="server" Text='<%# Bind("RFCDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn DataField="EmailFacturasDirFis" HeaderText="Email Facturas" UniqueName="EmailFacturasDirFis">
                                                                    <ItemTemplate> 
                                                                            <asp:Label ID="lblEmailFacturasDirFis" runat="server" Text='<%# Bind("EmailFacturasDirFis") %>'></asp:Label> 
                                                                     </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="txtEmailFacturasDirFis" runat="server" Text='<%# Bind("EmailFacturasDirFis") %>'
	                                                                     MaxLength="9" 
																				Width="100px">
                                                                    </telerik:RadTextBox></EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />                                                                    
                                                                    <HeaderStyle Width="50px" />
                                                              </telerik:GridTemplateColumn>


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
                                                        </MasterTableView>
                                                    </telerik:RadGrid>

                                        </table>
                                        

                                    </telerik:RadPane>
                                    </telerik:RadSplitter>

                            </telerik:RadPageView>
      </telerik:radmultipage>



      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function jsFunction() {
           

            }
            function AbrirVentana_Bitacora(Id_Cd, Id_Acys, Pantalla) {

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

            }


            function drop(e) {


            }


            function LlenaDatosCalendario() {

      

            }



     


            function onResize(sender, eventArgs) {

            }
            function pre_validarfecha() {

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
              
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {

                
            }

            function txt2_OnBlur(sender, args) {
               
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
             
            }

            function txt3_OnBlur(sender, args) {
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
               
            }

            function txt4_OnBlur(sender, args) {
               
            }

            function cmb4_ClientSelectedIndexChanged(sender, eventArgs) {

            
            }

            function txt5_OnBlur(sender, args) {
                
            }

            function cmb5_ClientSelectedIndexChanged(sender, eventArgs) {

               
            }

            function ObtenerControlFecha() {

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
//                var cerrarWindow = radalert(mensaje, 330, 150);
//                cerrarWindow.add_close(
                //                    function () {

                alert(mensaje);
                        CloseWindow();
//                    });
            }

            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            function TabSelected(sender, args) {
                
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
            }

            function onLoadIdPrd(sender)
            { txtId = sender; }
            function onLoadPrdDescr(sender)
            { txtDes = sender; }


            function AbrirCalendario(Id_TG) {
                var oWnd = radopen("VentanaCalendario.aspx?Id_TG=" + Id_TG);
                oWnd.center();
                oWnd.setSize(750, 560);

            }

        </script>
     </telerik:radcodeblock>
     

   
     
</asp:Content>

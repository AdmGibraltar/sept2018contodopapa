<%@ Page Title="Gastos" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapPagosElectronicosConsultar.aspx.cs" Inherits="SIANWEB.CapPagosElectoronicosConsultar" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <style>
        .RadInput input[readonly]
        {
            background-color: #F7F7F7 !important;
        }
        
        #PopUpBackground
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
            z-index: 100000;
        }
        
        #PopUpProgress
        {
            position: fixed;
            font-size: 120%;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('images/loading.gif');
            background-repeat: no-repeat;
            background-position: center;
            border-radius: .6em;
        }
    </style>
    <telerik:radwindowmanager runat="server" id="RadWindowManager1"></telerik:radwindowmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server">
        <div id="PopUpBackground"></div>
        <div id="PopUpProgress">
            <h6><p style="text-align:center;"><b>Favor de Esperar...</b></p></h6>
        </div>
    </telerik:radajaxloadingpanel>
    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
           </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="cmbProveedor">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>
          

           <telerik:AjaxSetting AjaxControlID="cmbTipo">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="cmbConcepto">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="btnAcredor">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"   />
               </UpdatedControls>
           </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkConComprobante">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>
         
             <telerik:AjaxSetting AjaxControlID="CmbSubTipoGasto">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>

            <%--JFCV agregue este botón --%>
            
            <telerik:AjaxSetting AjaxControlID="BtnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="UploadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUpload" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="FileUploadControl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divUpload" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             
        </AjaxSettings>
    </telerik:radajaxmanager>
    <div runat="server" id="divPrincipal">
        <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
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
        </telerik:radtoolbar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="divCtaGastos" runat="server">
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                        <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                    </td>
                    <td style="width: 160px">
                        <telerik:radcombobox id="CmbTipo" runat="server" onselectedindexchanged="CmbTipo_SelectedIndexChanged"
                            autopostback="True"></telerik:radcombobox>
                    </td>
                    <td>
                        <asp:Label ID="lblSubTipoGasto" runat="server" Text="SubTipo Gasto" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <telerik:radcombobox id="CmbSubTipoGasto" runat="server" autopostback="True" visible="False" onselectedindexchanged="CmbSubTipo_SelectedIndexChanged"  ReadOnly="true">
                            <Items>   
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />  
                                <telerik:RadComboBoxItem runat="server" Text="Flete" Value="1" />
                                <telerik:RadComboBoxItem runat="server" Text="No Inventariable" Value="2" />
                                <telerik:RadComboBoxItem runat="server" Text="Compra Local" Value="3" />
                                <telerik:RadComboBoxItem runat="server" Text="Pagos de Servicios" Value="4" />
                                <telerik:RadComboBoxItem runat="server" Text="Otros" Value="5" /> 
                                 <%-- JFCV 09 sep 2016 agregar dos subtipos de gasto --%>
                                <telerik:RadComboBoxItem runat="server" Text="Honorarios" Value="6" /> 
                                <telerik:RadComboBoxItem runat="server" Text="Arrendamientos" Value="7" /> 
                            </Items>    
                        </telerik:radcombobox>
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="lblPagoElectronico" runat="server" Text="Pago Elect."></asp:Label>
                    </td>
                    <td>
                        <telerik:radtextbox id="TxtPagoElectronico" runat="server" readonly="true"></telerik:radtextbox>
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblConComprobante" runat="server" Text="Con Comprobante"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkConComprobante" runat="server" 
                            AutoPostBack="true" />
                    </td>

                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblCtaGastos" runat="server" Text="Concept. Gto." display="false"></asp:Label>
                    </td>
                    <td colspan="6">

                     <asp:Label ID="lblcmbCtaGastos" runat="server" Text="Seleccione." display="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <%-- ///JFCV comentar para hacer mas espacio<br />--%>
            <asp:Panel ID="divProveedor" runat="server">
                <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc" width="837">
                    <tr>
                       
                <td>
                            <asp:Label ID="Label2" runat="server" Text="No. Proveedor" />
                        </td>
                        <td colspan="3">
                            <telerik:radcombobox id="cmbProveedor" runat="server" width="433px" onselectedindexchanged="cmbProveedor_SelectedIndexChanged"
                                  onclientfocus="cmdClient_Focus"
                                filter="Contains" changetextonkeyboardnavigation="true" markfirstmatch="true"
                                enableloadondemand="true" highlighttemplateditems="true" loadingmessage="Cargando..."
                                enableautomaticloadondemand="True" enablevirtualscrolling="True" itemsperrequest="12"
                                showmoreresultsbox="True" maxheight="400px" emptymessage="-- Seleccionar --"
                                autopostback="True" onclientblur="Combo_ClientBlur">
                                <HeaderTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td valign="middle" style="text-align:center">
                                                <b>Proveedor/Acreedor</b>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width:100%">
                                        <tr>
                                            <td valign="middle" style="text-align:left">
                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Acr").ToString() == "-1" ? "-- Seleccionar el Proveedor --": DataBinder.Eval(Container.DataItem, "Acr_Nombre") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;" NoMatches="No hay coincidencias" />
                            </telerik:radcombobox>
                        </td>
                    </tr>
                    <tr>
                        <%-- JFCV 29 octubre 2015 aqui el cambio que hice fue que donde decia cuenta debe mostrar el CC el campo de cuenta lo sigo usando asi que lo deje 
                   pero lo pongo mas abajo , cuenta es igual a lo que tenga en txtnumero--%>
                        <td>
                            <asp:Label ID="LblCuenta" runat="server" Text="CC"></asp:Label>
                        </td>
                        <td>
                            <telerik:radtextbox id="TxtCc" runat="server" readonly="true" visible="true"></telerik:radtextbox>
                        </td>
                        <td>
                            <asp:Label ID="LblSubCuenta" runat="server" Text="Sub Cuenta"></asp:Label>
                        </td>
                        <td>
                            <telerik:radtextbox id="TxtSubCuenta" runat="server" readonly="true"></telerik:radtextbox>
                        </td>
                        <td>
                            <asp:Label ID="LblSusSubCuenta" runat="server" Text="Sub Sub-Cta"></asp:Label>
                        </td>
                        <td>
                            <telerik:radtextbox id="TxtSubSubCuenta" runat="server" readonly="true"></telerik:radtextbox>
                            <telerik:radtextbox id="TxtCuenta" runat="server" readonly="true" visible="false"></telerik:radtextbox>
                            <asp:RequiredFieldValidator ID="RfvCuenta" runat="server" ControlToValidate="TxtCuenta"
                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            <telerik:radtextbox id="TxtNumero" runat="server" readonly="true" visible="false"></telerik:radtextbox>
                            <telerik:radnumerictextbox id="TxtCuentaPago" runat="server" visible="false"><NumberFormat DecimalDigits="0" /></telerik:radnumerictextbox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlEncabezadoGastosViaje" runat="server" Visible="false" span="1">
            <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
                <tr>
                    <td>
                        <asp:Label ID="LblSolicitanteViajero" runat="server" Text="Persona que Viaja"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:radtextbox id="TxtSolicitanteViajero" runat="server" width="200px"></telerik:radtextbox>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaElaboracion" runat="server" Text="Fecha Elaboración"></asp:Label>
                    </td>
                    <td>
                        <telerik:raddatepicker id="txtFechaElaboracion" runat="server" culture="es-MX" width="100px"
                            autopostback="true"></telerik:raddatepicker>
                    </td>
                    <%--JFCV 12 ene 2016 agregar destino--%>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblDestino" runat="server" Text="Destino "></asp:Label>
                    </td>
                    <td>
                        <telerik:radtextbox id="TxtDestino" runat="server"></telerik:radtextbox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblMotivo" runat="server" Text="Motivo del Viaje"></asp:Label>
                    </td>
                    <td>
                        <telerik:radtextbox id="TxtMotivo" runat="server"></telerik:radtextbox>
                    </td>
                    <td>
                        <asp:Label ID="LblFechaSalida" runat="server" Text="Fecha Salida"></asp:Label>
                    </td>
                    <td>
                        <telerik:raddatepicker id="TxtFechaSalida" runat="server" culture="es-MX" width="100px"
                            autopostback="true" onselecteddatechanged="TxtFechaSalida_SelectedDateChanged"></telerik:raddatepicker>
                    </td>
                    <td>
                        <asp:Label ID="LblFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label>
                    </td>
                    <td>
                        <telerik:raddatepicker id="TxtFechaRegreso" runat="server" culture="es-MX" width="100px"
                            autopostback="true" onselecteddatechanged="TxtFechaRegreso_SelectedDateChanged"></telerik:raddatepicker>
                    </td>
                    <td>
                        <asp:Label ID="LblCantidadDias" runat="server" Text="Dias de Viaje"></asp:Label>
                    </td>
                    <td>
                        <telerik:radtextbox id="TxtCantidadDias" runat="server" readonly="true"></telerik:radtextbox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSolicitudCheque" runat="server" Visible="false">
            <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc; width: 837px">
                <tr>
                    <td style="width: 5%;">
                    </td>
                    <td style="width: 15%;">
                        <asp:Label ID="LblSolicitante" runat="server" Text="Solicitante"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <telerik:radtextbox id="TxtSolicitante" runat="server" width="320px"></telerik:radtextbox>
                        <asp:RequiredFieldValidator ID="RfvSolicitante" runat="server" ControlToValidate="TxtSolicitante"
                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 25%;">
                        <asp:Label ID="LblFechaRequiere" runat="server" Text="Fecha en que se Requiere"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <telerik:raddatepicker id="TxtFechaRequiere" runat="server" culture="es-MX" width="100px"></telerik:raddatepicker>
                        <asp:RequiredFieldValidator ID="RfvFechaRequiere" runat="server" ControlToValidate="TxtFechaRequiere"
                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                        <telerik:radcombobox id="CmbTipoComprobanteSin" runat="server" onselectedindexchanged="CmbTipoComprobanteSin_SelectedIndexChanged"
                            autopostback="True">
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Con Comprobante" Value="true" /></Items>
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Sin Comprobante" Value="false" /></Items>
                                </telerik:radcombobox>
                    </td>
                    <td style="width: 15%;">
                       
                    </td>
                    <td style="width: 30%;">
                        <p>
                            
                        </p>
                    </td>
                    <td style="width: 10%;">
                        <asp:ImageButton ID="btnAcredor" runat="server" ImageUrl="~/Imagenes/blank.png" Style="margin: -10px 0px -7px 0px!important;
                            background: transparent url(Imagenes/Sprite01.png) no-repeat 0px -1152px; height: 17px;
                            width: 16px; margin-left: 0px;"  />
                    </td>
                    <td style="width: 25%;">
                        <asp:Label ID="lblTotalPagar" runat="server" Text="Total a Pagar" Width="100px"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <telerik:radnumerictextbox id="txtTotalAPagar" runat="server" width="150px" readonly="true"><NumberFormat DecimalDigits="2"/></telerik:radnumerictextbox>
                    </td>
                </tr>
            </table>
            <%--<br />--%>
            <table style="font-family: Verdana; font-size: 8pt; padding-top: 0">
                <tr>
                    <td colspan="12">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                     
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
          
            <table>
                <tr>
                    <td>
                        <telerik:radgrid id="rgPagoElectronico" runat="server" CommandItemDisplay="Top" gridlines="None" autogeneratecolumns="False"
                            onneeddatasource="RadGrid1_NeedDataSource" oninsertcommand="RadGrid1_InsertCommand"
                            onitemcommand="rgPagoElectronico_ItemCommand" EditMode="InPlace" OnItemDataBound="rgPagoElectronico_ItemDataBound"
                            onupdatecommand="RadGrid1_UpdateCommand" pagesize="6" allowpaging="True" datamember="listaOrdCompraDet">
                                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_GVComprobante,GVComprobante_ConComprobanteDescripcion,PagElec_Id_PagElecCuenta"
                                                    EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                                    NoMasterRecordsText="No se encontraron registros."  >
                                                    <CommandItemSettings ShowAddNewRecordButton="false"  />
                                                    <Columns>
                                                        
                                                        
                                                          <telerik:GridTemplateColumn HeaderText="Núm. Solicitud" DataField="Id_GVComprobante" UniqueName="Id_GVComprobante" ReadOnly="true" Display="false">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_GVComprobante" runat="server" Text='<%# Eval("Id_GVComprobante").ToString() %>'  ReadOnly="true" Display="false" />
                                                            </ItemTemplate>
                                                             
                                                        </telerik:GridTemplateColumn>
                                                      



                                                    <telerik:GridTemplateColumn HeaderText="Con/Sin" DataField="GVComprobante_ConComprobanteDescripcion" UniqueName="GVComprobante_ConComprobanteDescripcion"
                                                            Display="true">
                                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGVComprobante_ConComprobanteDescripcion" runat="server" Text='<%# Eval("GVComprobante_ConComprobanteDescripcion").ToString() %>' />
                                                            </ItemTemplate>
                                                          
                                                        </telerik:GridTemplateColumn>
                                                    

                                                    <%--  <telerik:GridBoundColumn DataField="" HeaderText="Importe"   UniqueName="GVComprobante_Importe" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >
                                                      <HeaderStyle Width="45" /></telerik:GridBoundColumn>--%>
                                                         <telerik:GridTemplateColumn HeaderText="Importe" DataField="GVComprobante_Importe" UniqueName="GVComprobante_Importe">
                                                        <HeaderStyle Width="45px" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGVComprobante_Importe" runat="server" Text='<%# Eval("GVComprobante_Importe").ToString() %>' DataFormatString="{0:N2}" />
                                                        </ItemTemplate> <EditItemTemplate></EditItemTemplate></telerik:GridTemplateColumn>
                                  
                                                          <telerik:GridTemplateColumn DataField="GVComprobante_Fecha" HeaderText="Fecha" UniqueName="GVComprobante_Fecha">
                                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGVComprobante_Fecha" runat="server" Text='<%# Convert.ToDateTime(Eval("GVComprobante_Fecha")).ToString() %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                               <%-- <telerik:RadDatePicker ID="txtGVComprobante_Fecha" runat="server" DbSelectedDate='<%# Eval("GVComprobante_Fecha") %>'
                                                                    Width="150px" Enabled="false">
                                                                </telerik:RadDatePicker>--%>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Observaciones" DataField="GVComprobante_Observaciones" UniqueName="GVComprobante_Observaciones">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGVComprobante_Observaciones" runat="server" Text='<%# Eval("GVComprobante_Observaciones").ToString() %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadTextBox ID="txtGVComprobante_Observaciones" runat="server" Width="65px" MaxLength="100"
                                                                     AutoPostBack="true"  Text='<%# Eval("GVComprobante_Observaciones") %>'>
                                                                   
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadTextBox>
                                                                <asp:Label ID="lblVal_txtGVComprobante_Observaciones" runat="server" ForeColor="#FF0000" Text='<%# Eval("GVComprobante_Observaciones").ToString() %>'
                                                                    Visible="false"></asp:Label>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
              
                             <telerik:GridTemplateColumn HeaderText="PagElec_Id_PagElecCuenta" DataField="PagElec_Id_PagElecCuenta" UniqueName="Id_PagElec_Id_PagElecCuenta" ReadOnly="true" Display="false">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_PagElec_Id_PagElecCuenta" runat="server" Text='<%# Eval("PagElec_Id_PagElecCuenta").ToString() %>'  ReadOnly="true" Display="false" />
                                                            </ItemTemplate>
                             </telerik:GridTemplateColumn>                                                      
                            <telerik:GridBoundColumn DataField="PagElec_Id_PagElecCuenta" HeaderText="PagElec_Id_PagElecCuenta" UniqueName="PagElec_Id_PagElecCuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Rfc" HeaderText="PagElec_Rfc" UniqueName="PagElec_Rfc" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_CuentaPago" HeaderText="PagElec_CuentaPago" UniqueName="PagElec_CuentaPago" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Cuenta" DataField="PagElec_Cc" UniqueName="PagElec_SubSubCuenta">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblPagElec_Cc" runat="server" Text='<%# Eval("PagElec_Cc").ToString() %>' />
                            </ItemTemplate> <EditItemTemplate></EditItemTemplate></telerik:GridTemplateColumn>
 

                            <telerik:GridBoundColumn DataField="PagElec_Numero" HeaderText="Cuenta" UniqueName="PagElec_Numero" Visible="false"><HeaderStyle Width="25" /></telerik:GridBoundColumn>
                        

                            <telerik:GridTemplateColumn HeaderText="Sub Cuenta" DataField="PagElec_SubCuenta" UniqueName="PagElec_SubCuenta">
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblPagElec_SubCuenta" runat="server" Text='<%# Eval("PagElec_SubCuenta").ToString() %>' />
                            </ItemTemplate> 
                            
                            <EditItemTemplate>
                                                             
                                            <telerik:RadComboBox id="txtcmbCtaGastos" runat="server" width="280px" onselectedindexchanged="txtcmbCtaGastos_SelectedIndexChanged"
                                             onclientfocus="cmdClient_Focus" filter="Contains" changetextonkeyboardnavigation="true" enablevirtualscrolling="True" loadingmessage="Cargando..." >
                                        <HeaderTemplate>
                                            <table style="width:100%">
                                                <tr>
                                                    <td valign="middle" style="width: 150px; text-align: left">
                                                        <b>Descripcion</b>
                                                    </td>
                                                    <td valign="middle" style="width: 50px; text-align: left">
                                                        <b>SubCta</b>
                                                    </td>
                                                    <td valign="middle" style="width: 50px; text-align: left">
                                                        <b>SubSubCta</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%">
                                                <tr>
                                                    <td valign="middle" style="width: 150px; text-align: left">
                                                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_Descripcion") %>' />
                                                    </td>
                                                    <td valign="middle" style="width: 50px; text-align: left">
                                                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubCuenta") %>' />
                                                    </td>
                                                    <td valign="middle" style="width: 50px; text-align: left">
                                                        <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubSubCuenta") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                            NoMatches="No hay coincidencias" />
                                    </telerik:RadComboBox>

                                        <asp:Label ID="lblVal_cmbTipo" runat="server" ForeColor="#FF0000"></asp:Label>
                                    </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="Sub Sub-Cta" DataField="PagElec_SubSubCuenta" UniqueName="PagElec_SubSubCuenta">
                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblPagElec_SubSubCuenta" runat="server" Text='<%# Eval("PagElec_SubSubCuenta").ToString() %>' />
                            </ItemTemplate> <EditItemTemplate></EditItemTemplate></telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="Serie" DataField="PagElec_Serie" UniqueName="PagElec_Serie">
                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblPagElec_Serie" runat="server" Text='<%# Eval("PagElec_Serie").ToString() %>' />
                            </ItemTemplate> <EditItemTemplate></EditItemTemplate></telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="Folio" DataField="PagElec_Folio" UniqueName="PagElec_Folio">
                            <HeaderStyle Width="25px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblPagElec_Folio" runat="server" Text='<%# Eval("PagElec_Folio").ToString() %>' />
                            </ItemTemplate> <EditItemTemplate></EditItemTemplate></telerik:GridTemplateColumn>
 
                            <telerik:GridBoundColumn DataField="PagElec_UUID" HeaderText="PagElec_UUID" UniqueName="PagElec_UUID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Subtotal" HeaderText="PagElec_Subtotal" UniqueName="PagElec_Subtotal" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Iva" HeaderText="PagElec_Iva" UniqueName="PagElec_Iva" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_ImpRetenido" HeaderText="PagElec_ImpRetenido" UniqueName="PagElec_ImpRetenido" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_IvaRetenido" HeaderText="PagElec_IvaRetenido" UniqueName="PagElec_IvaRetenido" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Xml" HeaderText="GVComprobante_Xml" UniqueName="GVComprobante_Xml" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Pdf" HeaderText="GVComprobante_Pdf" UniqueName="GVComprobante_Pdf" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Ruta" HeaderText="GVComprobante_Ruta" UniqueName="GVComprobante_Ruta" Visible="false"></telerik:GridBoundColumn>



                                                      
                                                         
                                                       
                                                
                                                     

                                            <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="30px"
                                                UniqueName="XML">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Descargar" CommandName="XML" Visible ='<%#Eval("GVComprobante_Xml") != "" ? true:false%>'/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="30px"
                                                UniqueName="PDF">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>


                                                       </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="15" />
                                            </telerik:radgrid>
                    </td>
                </tr>
            </table>
        
        </asp:Panel>
        <asp:Panel ID="PnlGastosViaje" runat="server" Visible="false">
            <table id="TblTotales">
                <tr>
                    <td valign="top">
                        <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransporte" runat="server" Text="Tipo Transporte"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="CmbTipoComprobante" runat="server">
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Autobus" Value="1" /></Items>
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Avion" Value="2" /></Items>
                                </telerik:radcombobox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtTransporteCuota" runat="server"><ClientEvents OnBlur="OnBlurImporte" /><NumberFormat DecimalDigits="2" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    NO. DIAS
                                </td>
                                <td>
                                    CUOTA DIARIA
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblHospedaje" runat="server" Text="Hospedaje"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtHospedajeDias" runat="server" class="presupuesto"><ClientEvents OnBlur="OnBlurImporte" /></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtHospedajeCutoa" runat="server" readonly="true"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtHospedajeImporte" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    ALIMENTACION
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblDesayunos" runat="server" Text="Desayunos"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtDesayunosDias" runat="server" class="presupuesto"><ClientEvents OnBlur="OnBlurImporte" /></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtDesayunosCutoa" runat="server" readonly="true"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtDesayunosImporte" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblComidas" runat="server" Text="Comidas"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtComidasDias" runat="server"><ClientEvents OnBlur="OnBlurImporte" /></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtComidasCutoa" runat="server" readonly="true"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtComidasImporte" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblCenas" runat="server" Text="Cenas"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtCenasDias" runat="server"><ClientEvents OnBlur="OnBlurImporte" /></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtCenasCutoa" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtCenasImporte" runat="server" readonly="true">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblOtrosGastos" runat="server" Text="Otros Gastos"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtOtrosGastosDias" runat="server"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="OnBlurImporte" /></telerik:radnumerictextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtOtrosGastosCutoa" runat="server"><NumberFormat DecimalDigits="2" GroupSeparator="" /><ClientEvents OnBlur="OnBlurImporte" /></telerik:radnumerictextbox>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtOtrosGastosImporte" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotal" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="TxtTotal" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100%; visibility: hidden" valign="top">
                        <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
                            <tr>
                                <td colspan="2" style="border: 1px solid #ccc">
                                    Detalle de Otros Gastos:
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #ccc">
                                    Concepto
                                </td>
                                <td style="border: 1px solid #ccc">
                                    Valor
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion1" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor1" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion2" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor2" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion3" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor3" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion4" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor4" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion5" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor5" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion6" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor6" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion7" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor7" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoDescripcion8" runat="server" width="180px"></telerik:radtextbox>
                                </td>
                                <td>
                                    <telerik:radtextbox id="TxtOtroGastoValor8" runat="server"></telerik:radtextbox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="HF_AnticipoPorComprobar" runat="server" Value="False" />
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
        <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
       
        <script type="text/javascript">
            function OnBlurImporte(sender, eventArgs) {
                Calcular_Importes();
                //alert(sender.get_id().split('_')[2]);
            }

            function Calcular_Importes() {


                //desayuno

                var txtDesayunosDias = $find('<%= TxtDesayunosDias.ClientID %>');
                var txtDesayunosCutoa = $find('<%= TxtDesayunosCutoa.ClientID %>');
                var txtDesayunosImporte = $find('<%= TxtDesayunosImporte.ClientID %>');

                var txtTotal = $find('<%= TxtTotal.ClientID %>');


                if (txtDesayunosDias.get_value() == '') {
                    txtDesayunosDias.set_value(0);
                    txtDesayunosImporte.set_value(0);
                }
                else {
                    txtDesayunosImporte.set_value(txtDesayunosDias.get_value() * txtDesayunosCutoa.get_value());
                }

                //comida;
                var TxtComidasDias = $find('<%= TxtComidasDias.ClientID %>');
                var TxtComidasCutoa = $find('<%= TxtComidasCutoa.ClientID %>');
                var TxtComidasImporte = $find('<%= TxtComidasImporte.ClientID %>');

                if (TxtComidasDias.get_value() == '') {
                    TxtComidasDias.set_value(0);
                    TxtComidasImporte.set_value(0);
                }
                else {
                    TxtComidasImporte.set_value(TxtComidasDias.get_value() * TxtComidasCutoa.get_value());
                }


                //hospedaje

                var txtHospedajeDias = $find('<%= TxtHospedajeDias.ClientID %>');
                var txtHospedajeCutoa2 = $find('<%= TxtHospedajeCutoa.ClientID %>');
                var txtHospedajeImporte = $find('<%= TxtHospedajeImporte.ClientID %>');
                var txtHospedajeCutoa = 1000;

                if (isNaN(parseFloat(txtHospedajeDias.get_value()))) {
                    txtHospedajeDias.set_value(0);
                    txtHospedajeImporte.set_value(0);
                }
                else {
                    txtHospedajeImporte.set_value(txtHospedajeDias.get_value() * txtHospedajeCutoa);
                }

                txtHospedajeImporte.set_value(txtHospedajeDias.get_value() * txtHospedajeCutoa);

                //cenas;
                var txtCenasDias = $find('<%= TxtCenasDias.ClientID %>');
                var txtCenasCutoa = $find('<%= TxtCenasCutoa.ClientID %>');
                var txtCenasImporte = $find('<%= TxtCenasImporte.ClientID %>');



                if (txtCenasDias.get_value() == '') {
                    txtCenasDias.set_value(0);
                    txtCenasImporte.set_value(0);
                }
                else {
                    txtCenasImporte.set_value(txtCenasDias.get_value() * txtCenasCutoa.get_value());
                }


                //otros gastos;
                var txtOtrosGastosDias = $find('<%= TxtOtrosGastosDias.ClientID %>');
                var txtOtrosGastosCutoa = $find('<%= TxtOtrosGastosCutoa.ClientID %>');
                var txtOtrosGastosImporte = $find('<%= TxtOtrosGastosImporte.ClientID %>');

                var txtTransporteCuota = $find('<%= TxtTransporteCuota.ClientID %>');





                if (txtOtrosGastosDias.get_value() == '') {
                    txtOtrosGastosDias.set_value(0);
                    txtOtrosGastosImporte.set_value(0);
                }
                else {
                    txtOtrosGastosImporte.set_value(txtOtrosGastosDias.get_value() * txtOtrosGastosCutoa.get_value());
                }

                //calcular  totales 

                if (isNaN(parseFloat(txtDesayunosImporte.get_value()))) {
                    txtDesayunosImporte.set_value(0);
                }
                if (isNaN(parseFloat(TxtComidasImporte.get_value()))) {
                    TxtComidasImporte.set_value(0);
                };
                if (isNaN(parseFloat(txtCenasImporte.get_value()))) {
                    txtCenasImporte.set_value(0);
                };
                if (isNaN(parseFloat(txtOtrosGastosImporte.get_value()))) {
                    txtOtrosGastosImporte.set_value(0);
                };
                if (isNaN(parseFloat(txtHospedajeImporte.get_value()))) {
                    txtHospedajeImporte.set_value(0);
                };
                if (isNaN(parseFloat(txtTransporteCuota.get_value()))) {
                    txtTransporteCuota.set_value(0);
                };

                txtTotal.set_value(txtHospedajeImporte.get_value() + txtDesayunosImporte.get_value() + TxtComidasImporte.get_value() + txtCenasImporte.get_value() + txtOtrosGastosImporte.get_value() + txtTransporteCuota.get_value());

                $('#<%= TxtTotal.ClientID %>').val(parseFloat(txtHospedajeImporte.get_value() + txtDesayunosImporte.get_value() + TxtComidasImporte.get_value() + txtCenasImporte.get_value() + txtTransporteCuota.get_value() + txtOtrosGastosImporte.get_value()).toFixed(2));
            }


            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            function CloseWindow() {
                GetRadWindow().Close();
                //JFCV 18oct2016 que cierre la pantalla al grabar y si no graba que pregunte si desea salir control cambio 9
                var oWnd = GetRadWindow();
                oWnd.close();
                top.location.href = top.location.href;
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
 

            function cmbProveedor_Focus(sender, args) {
                var cid = "<%=cmbProveedor.ClientID %>" + "_Input";
                var input = document.getElementById(cid);

                input.value = "";
            }


            function cmdClient_Focus(sender, args) {
                var input = sender.get_inputDomElement();
                sender.highlightAllMatches(input.defaultValue);
                sender.showDropDown();
            }

           
           

        
        </script>

        <%--//JFCV 14 ene 2016--%>

                <script type="text/javascript">
                    function ShowPopup(message, id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, pagElecCuenta_Descripcion, acmbTipo, cmbSubTipoGasto) {
                        function confirmaFn(arg) {
                            if (arg) {
                                PageMethods.ProcesoEnviar(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, pagElecCuenta_Descripcion, acmbTipo, cmbSubTipoGasto);
                                CloseAlert('El gasto se envío a Autorizar.');
                            }
                            else {
                                CloseAlert('El gasto se registro correctamente.');
                            }
                        }
                        radconfirm(message, confirmaFn);
                        
                    }
                    <%--jfcv 18oct2016 que pregunte antes de salir control de cambio 9 --%>
                    function closeWin() {
                        var oWnd = GetRadWindow();
                        oWnd.close();
                        top.location.href = top.location.href;
                    }

                 </script>



    
    </telerik:radcodeblock>
</asp:Content>


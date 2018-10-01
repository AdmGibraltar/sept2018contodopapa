<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true"
    CodeBehind="CapGastosViajeComprobacion.aspx.cs" Inherits="SIANWEB.CapGastosViajeComprobacion" %>

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
     <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" 
        onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbDesasingar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbProgramaReparto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbAsignar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cmbProveedor">
               <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
               </UpdatedControls>
           </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="cmbCtaGastos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divCtaGastos" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radwindowmanager id="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:radwindowmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
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
        <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
            <tr>
                <td>
                    <asp:Label ID="LblSolicitanteViajero" runat="server" Text="Solicitante"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:radtextbox id="TxtSolicitanteViajero" runat="server" width="200px" readonly="true"></telerik:radtextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblMotivo" runat="server" Text="Motivo"></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="TxtMotivo" runat="server" readonly="true"></telerik:radtextbox>
                </td>
                <td>
                    <asp:Label ID="LblFechaSalida" runat="server" Text="Fecha Salida"></asp:Label>
                    <asp:Label ID="LblFechaRequiere" runat="server" Text="Fecha" Visible="false"></asp:Label>
                </td>
                <td>
                    <telerik:raddatepicker id="TxtFechaSalida" runat="server" culture="es-MX" width="100px"
                        autopostback="true" readonly="true"></telerik:raddatepicker>
                    <telerik:raddatepicker id="TxtFechaRequiere" runat="server" ulture="es-MX" width="100px"
                        visible="false" readonly="true"></telerik:raddatepicker>
                </td>
                <td>
                    <asp:Label ID="LblFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label>
                </td>
                <td>
                    <telerik:raddatepicker id="TxtFechaRegreso" runat="server" culture="es-MX" width="100px"
                        autopostback="true" readonly="true"></telerik:raddatepicker>
                </td>
                <td>
                    <asp:Label ID="LblCantidadDias" runat="server" Text="Dias de Viaje"></asp:Label>
                </td>
                <td>
                    <telerik:radnumerictextbox id="TxtCantidadDias" runat="server" readonly="true"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:radnumerictextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblImporteSolicitado" runat="server" Text="Importe Solicitado"></asp:Label>
                </td>
                <td>
                    <telerik:radnumerictextbox id="TxtImporteSolicitado" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="LblImporteComprobado" runat="server" Text="Importe Comprobado"></asp:Label>
                </td>
                <td>
                    <telerik:radnumerictextbox id="TxtImporteComprobado" runat="server" readonly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="LblSaldoFavor" runat="server" Text="A Mi Favor"></asp:Label>
                </td>
                <td id="saldofavor">
                    <telerik:radnumerictextbox id="TxtSaldoFavor" runat="server" readonly="true"  font-color= "blue" ><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                </td>
            </tr>
        </table>
        <br />
        <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="LblConcepto" runat="server" Text="Concept. Gto."></asp:Label>
                                <telerik:radcombobox id="cmbCtaGastos" runat="server" width="684px" onclientfocus="cmdClient_Focus"
                                    onselectedindexchanged="cmbCtaGastos_SelectedIndexChanged" filter="Contains"
                                    changetextonkeyboardnavigation="true" markfirstmatch="true" enableloadondemand="true"
                                    highlighttemplateditems="true" loadingmessage="Cargando..." enableautomaticloadondemand="True"
                                    enablevirtualscrolling="True" itemsperrequest="12" showmoreresultsbox="True"
                                    maxheight="300px" emptymessage="-- Seleccionar --" autopostback="True">
                                    <HeaderTemplate>
                                        <table style="width:100%">
                                            <tr>
                                                <td valign="middle" style="width: 300px; text-align: left">
                                                    <b>Descripcion</b>
                                                </td>
                                                <td valign="middle" style="width: 100px; text-align: left">
                                                    <b>Sub Cuenta</b>
                                                </td>
                                                <td valign="middle" style="width: 100px; text-align: left">
                                                    <b>SubSub Cuenta</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width:100%">
                                            <tr>
                                                <td valign="middle" style="width: 300px; text-align: left">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_Descripcion") %>' />
                                                </td>
                                                <td valign="middle" style="width: 100px; text-align: left">
                                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubCuenta") %>' />
                                                </td>
                                                <td valign="middle" style="width: 100px; text-align: left">
                                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubSubCuenta") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                        NoMatches="No hay coincidencias" />
                                </telerik:radcombobox>
                                <%--<telerik:RadCombobox ID="CmbConcepto" runat="server" OnSelectedIndexChanged="CmbConcepto_SelectedIndexChanged" autopostback="false">
                                </telerik:RadCombobox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="font-family: Verdana; font-size: 8pt; border: 1px solid #ccc">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCuenta" runat="server" Text="Cuenta"></asp:Label>
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
                                           <%-- JFCV la cuenta no la pide al grabar ya no se ocupa porque es por cada comprobante que se pone la cuenta <asp:RequiredFieldValidator ID="RfvCuenta" runat="server" ControlToValidate="TxtCuenta"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>--%>
                                            <telerik:radtextbox id="TxtNumero" runat="server" readonly="true" visible="false"></telerik:radtextbox>
                                            <telerik:radnumerictextbox id="TxtCuentaPago" runat="server" visible="false"><NumberFormat DecimalDigits="0" /></telerik:radnumerictextbox>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDestino" runat="server" Text="Destino "></asp:Label>
                                        </td>
                                        <td>
                                             <telerik:radtextbox id="TxtDestino" runat="server" readonly="true"></telerik:radtextbox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <telerik:radcombobox id="CmbTipoComprobante" runat="server" onselectedindexchanged="CmbTipoComprobante_SelectedIndexChanged"
                                        autopostback="True">
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Con Comprobante" Value="true" /></Items>
                                    <Items><telerik:RadComboBoxItem runat="server" Text="Sin Comprobante" Value="false" /></Items>
                                </telerik:radcombobox>
                                    <asp:Label ID="LblFechaComprobante" runat="server" Text="Fecha"></asp:Label>
                                    <telerik:raddatepicker id="TxtFechaComprobante" runat="server" culture="es-MX" width="100px"
                                        readonly="true"></telerik:raddatepicker>
                                    <%--                            </td>
                        </tr>

                        <tr>
                            <td>--%>
                                    <label for="<%=cmbProveedor.ClientID %>">
                                        RFC del Emisor:</label>
                                    <telerik:radcombobox id="cmbProveedor" runat="server" width="365px" onselectedindexchanged="cmbProveedor_SelectedIndexChanged"
                                        onclientfocus="cmdClient_Focus" filter="Contains" changetextonkeyboardnavigation="true"
                                        markfirstmatch="true" enableloadondemand="true" highlighttemplateditems="true"
                                        loadingmessage="Cargando..." enableautomaticloadondemand="True" enablevirtualscrolling="True"
                                        itemsperrequest="12" showmoreresultsbox="True" maxheight="300px" emptymessage="-- Seleccionar --"
                                        autopostback="True">
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
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="12">
                    <asp:Panel ID="pnlFacturas" runat="server">
                        <telerik:radlistbox runat="server" id="cmbFactura" appenddatabounditems="true" onitemdatabound="cmbFactura_ItemDataBound"
                            allowtransfer="true" transfertoid="RadListBoxDestination" buttonsettings-areawidth="35px"
                            height="150px" width="380px" onclienttransferred="onClientTransferredHandler">
                            <HeaderTemplate>
                                    <table style="width:100%">
                                    <tr>
                                        <td>
                                            <b>Facturas Disponibles</b>
                                        </td>
                                    </tr>
                                    </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="divHeader" runat="server" class="rcbHeader" style="color: Black; width: 350px">
                                    <b><i><%# Eval("nombre")%></i></b>
                                </div>
                                <div id="divItem" runat="server" style="width: 350px">
                                    <table style="width: 100%; text-align: left">
                                        <tr>
                                            <td style="width: 45%">
                                                <asp:label ID="lblcmbFactura" runat="server" 
                                                Text='&nbsp;&nbsp;<%# String.Format(
                                                                "{0}{1} Fecha:{2} Importe:{3}",
                                                                Eval("Serie"),
                                                                Eval("Folio_Documento"),
                                                                Convert.ToDateTime(Eval("Fecha_Documento")).ToString("MM/dd/yyyy"),
                                                                Convert.ToDecimal(Eval("Importe_Total_Documento")).ToString("C")
                                                            ) %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </telerik:radlistbox>
                        <telerik:radlistbox runat="server" id="RadListBoxDestination" height="150px" width="380px"
                            buttonsettings-areawidth="35px" allowautomaticupdates="True">
                            <HeaderTemplate>
                                    <table style="width:100%">
                                    <tr>
                                        <td>
                                            <b>Facturas para Asociar</b>
                                        </td>
                                    </tr>
                                    </table>
                            </HeaderTemplate>
                        </telerik:radlistbox>
                    </asp:Panel>
                    <asp:Panel ID="pnlSoporte" runat="server" Visible="false">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50px;">
                                    <asp:Label ID="lblSoporte" runat="server" Text="Soporte:"></asp:Label>
                                </td>
                                <td style="width: 300px">
                                    <telerik:radasyncupload id="RadUpload1" runat="server" maxfileinputscount="1" overwriteexistingfiles="true"
                                        controlobjectsvisibility="RemoveButtons" localization-select="seleccione" width="245px"
                                        localization-remove="quitar" onclientfileuploaded="OnClientFileUploading">
                                    </telerik:radasyncupload>
                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="lblImporteSoporte" runat="server" Text="Importe:"></asp:Label>
                                    <telerik:radnumerictextbox id="TxtImporteSoporte" runat="server">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnValueChanged="TxtImporteSoporte_onBlur" />
                                    </telerik:radnumerictextbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="X-Small" Text=" "
                                        ForeColor="#000099"></asp:Label>
                                </td>
                                <td>
                                    <div id="Boton" style="visibility: collapse; width=1;">
                                        <asp:Button ID="buttonSubmit" runat="server" Text="Subir Archivo" Style="margin-top: 6px"
                                            OnClick="btnText_Click" />
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="LblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                            </td>
                            <td>
                                <telerik:radtextbox id="TxtObservaciones" runat="server" width="300px"></telerik:radtextbox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Button ID="BtnAgregar" runat="server" Text="Agregar Comprobante" OnClick="BtnAgregar_Click" />
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="rgPagoElectronico" runat="server" autogeneratecolumns="False"
                        gridlines="None" onneeddatasource="rgPagoElectronico_NeedDataSource" onitemcommand="rgPagoElectronico_ItemCommand"
                        pagesize="10" allowpaging="false" mastertableview-nomasterrecordstext="No se encontraron registros. "
                        height="200px">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_GVComprobante" HeaderText="Núm. Solicitud" UniqueName="Id_GVComprobante" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_ConComprobanteDescripcion" HeaderText="Con/Sin" UniqueName="GVComprobante_ConComprobanteDescripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%-- //JFCV  la columna de descripción ya no se va a mostrar en el grid <telerik:GridBoundColumn DataField="GVComprobanteTipo_Descripcion" HeaderText="Concepto" UniqueName="GVComprobanteTipo_Descripcion"><HeaderStyle Width="180" /></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="GVComprobante_Importe" HeaderText="Importe" UniqueName="GVComprobante_Importe" DataFormatString="{0:N2}"><HeaderStyle Width="55" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Fecha" HeaderText="Fecha" UniqueName="GVComprobante_Fecha" DataFormatString="{0:dd/MM/yy}"><HeaderStyle Width="55" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Observaciones" HeaderText="Observaciones" UniqueName="GVComprobante_Observaciones"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_CuentaPago" HeaderText="GVComprobante_GV_CuentaPago" UniqueName="GVComprobante_GV_CuentaPago" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_Cuenta" HeaderText="Número" UniqueName="GVComprobante_GV_Cuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_Cc" HeaderText="Cuenta" UniqueName="GVComprobante_GV_Cc" ><HeaderStyle Width="32" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_Numero" HeaderText="Cuentainvisible" UniqueName="GVComprobante_GV_Numero" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_SubCuenta" HeaderText="Sub Cuenta" UniqueName="GVComprobante_GV_SubCuenta"><HeaderStyle Width="35" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_GV_SubSubCuenta" HeaderText="Sub Sub-Cta" UniqueName="GVComprobante_GV_SubSubCuenta"><HeaderStyle Width="40" /></telerik:GridBoundColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="XML">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Descargar" CommandName="XML" Visible ='<%#Eval("GVComprobante_Xml") != "" ? true:false%>'/>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="PDF">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea eliminar el comprobante?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:radgrid>
                </td>
            </tr>
            
           
        </table>
       
       <div id="dialogoOculto" title="Esto es oculto " hidden="hidden"> 
                    <%--agregar un campo de control para lo de los grids--%>
                    <telerik:radnumerictextbox id="TxtImporteEnGrid" runat="server" readonly="true" width="0"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:radnumerictextbox>
                    </div>

       <%-- <div id="dialog">
     
       <input type="text" id="ConfirmBit" runat="server" name="txt" size="15" />
        <telerik:radtextbox id="Txtconfirma" runat="server" readonly="true"></telerik:radtextbox>
        </div>--%>
        </div>
  
  <div>
    <asp:HiddenField ID="HF_tipoPago" runat="server" Value="0"/>
    
  </div>
    
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
      
        <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
        <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
       
        <script type="text/javascript">

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
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
            //Cierra la venata actual y regresa el foco a la ventana padre
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
            function onClientTransferredHandler(sender, e) {
                var listBox = $find("<%=RadListBoxDestination.ClientID %>");
                var TxtImporteComprobado = $find("<%=TxtImporteComprobado.ClientID %>");
                //alert(parseFloat(TxtImporteComprobado.get_value()));
                var TxtImporteSolicitado = $find("<%=TxtImporteSolicitado.ClientID %>");
                var TxtSaldoFavor = $find("<%=TxtSaldoFavor.ClientID %>");
                var LblSaldoFavor = $telerik.$("#<%=LblSaldoFavor.ClientID %>");

                items = listBox.get_items();

                var TxtImporteEnGrid = $find("<%=TxtImporteEnGrid.ClientID %>");
//              Original 08feb2016  var Total = parseFloat(TxtImporteComprobado.get_value());

                var Total = parseFloat(TxtImporteEnGrid.get_value());
                $.each(items._array, function (i, item) {
                    var value = item.get_value().split("|");

                    Total += parseFloat(value[6]);
                });

 

                TxtImporteComprobado.set_value(Total);

                var diferencia = parseFloat(Total) - parseFloat(TxtImporteSolicitado.get_value());
                TxtSaldoFavor.set_value(diferencia);



                TxtSaldoFavor.get_styles().EnabledStyle[0] += "background-color: red;";
                TxtSaldoFavor.updateCssClass();


//                $("#TxtSaldoFavor").css("color", "red");
//                $("#LblSaldoFavor").css("color", "red");
//                $("#saldofavor").css("border", "3px solid red");
//                $("#saldofavor").css("background-color", "red");
//                $("#saldofavor").css("color", "red");
//                
               


////                LblSaldoFavor.text(diferencia < 0 ? "A mi Cargo" : "A Mi Favor");

//                TxtSaldoFavor.set_value(diferencia < 0 ? diferencia * -1 : diferencia);
                //                LblSaldoFavor.text(diferencia < 0 ? "A mi Cargo" : "A Mi Favor");
     

//                TxtSaldoFavor.get_styles().EnabledStyle[0] += "background-color: blue";
//                sender.get_styles().EnabledStyle[0] += "background-color: blue";
//                alert("despues de cambiarlo");

            }

            function TxtImporteSoporte_onBlur(sender, eventArgs) {
//                if (eventArgs.get_newValue() == "") {
//                    eventArgs.set_cancel(true);
//                    return;
//                }
                var listBox = $find("<%=RadListBoxDestination.ClientID %>");
                var TxtImporteComprobado = $find("<%=TxtImporteComprobado.ClientID %>");
                var TxtImporteSolicitado = $find("<%=TxtImporteSolicitado.ClientID %>");
                var TxtSaldoFavor = $find("<%=TxtSaldoFavor.ClientID %>");
//                var LblSaldoFavor = $telerik.$("#<%=LblSaldoFavor.ClientID %>");
                //JFCV que calcule los montos de los demás comprobantes no solo el de ete movimiento

                //JFCV agregar el textbox de txtImporteEnGrid 
                var TxtImporteEnGrid = $find("<%=TxtImporteEnGrid.ClientID %>");
                //              Original 08feb2016  var Total = parseFloat(TxtImporteComprobado.get_value());
                var Total = parseFloat(TxtImporteEnGrid.get_value());

                if (eventArgs.get_newValue() != "") {
                   Total = Total + parseFloat(eventArgs.get_newValue());
                }

                if (listBox != null) {
                    items = listBox.get_items();
                    $.each(items._array, function (i, item) {
                        var value = item.get_value().split("|");

                        Total += parseFloat(value[6]);
                    });
                    
                }

                TxtImporteComprobado.set_value(Total);

                var diferencia = parseFloat(Total) - parseFloat(TxtImporteSolicitado.get_value());
                TxtSaldoFavor.set_value(diferencia);

              

//                var diferencia = parseFloat(Total) - parseFloat(TxtImporteSolicitado.get_value());
//                TxtSaldoFavor.set_value(diferencia < 0 ? diferencia * -1 : diferencia);
//                LblSaldoFavor.text(diferencia < 0 ? "A mi Cargo" : "A Mi Favor");


                // TxtImporteComprobado.set_value(eventArgs.get_newValue());
                //JFCV fin 
                //var diferencia = parseFloat(eventArgs.get_newValue()) - parseFloat(TxtImporteSolicitado.get_value());
                // TxtSaldoFavor.set_value(diferencia < 0 ? diferencia * -1 : diferencia);
                // LblSaldoFavor.text(diferencia < 0 ? "A mi Cargo" : "A Mi Favor");
//                TxtSaldoFavor.get_styles().EnabledStyle[0] += "background-color: blue";
//                sender.get_styles().EnabledStyle[0] += "background-color: blue";
//                alert("despues de cambio");
            }

            function OnClientFileUploading(sender, args) {
                $("#<%=buttonSubmit.ClientID%>").click();
                //                $("#buttonSubmit").click(); 
            }

        </script>

 

           <script type="text/javascript">
                   function ShowPopup(message, id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, tipocomprobacion) {
                   function confirmaFn(arg) {
                       if (arg) {
                           PageMethods.ProcesoEnviar(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, tipocomprobacion);
                           CloseAlert('La comprobación se envío a Autorizar.');
                       }
                       else {
                           CloseAlert('La comprobación se registro correctamente.');
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
       <%-- <script type="text/javascript">
               function ShowPopup(message, id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, tipocomprobacion) {
                   $("#dialog").html(message);
                   $("#dialog").show();
                   $("#dialog").dialog({
                       title: "Enviar a Autorizar",
                       buttons: {
                           "Si": function () {
                               $('#Txtconfirma').val("SI");
                               PageMethods.ProcesoEnviar(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, tipocomprobacion);
                               $(this).dialog('close');
                               CloseAlert('La comprobación se envío a Autorizar.');
                           },
                           "No": function () {
                               $('#Txtconfirma').val("NO");
                               $(this).dialog('close');
                               CloseAlert('La comprobación se registro correctamente.');
                           }
                       },
                       modal: true
                   });

               }
               
              
   </script>--%>

    </telerik:radcodeblock>
</asp:Content>


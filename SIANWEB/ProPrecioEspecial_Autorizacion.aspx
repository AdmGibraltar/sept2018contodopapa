<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="ProPrecioEspecial_Autorizacion.aspx.cs" Inherits="SIANWEB.ProPrecioEspecial_Autorizacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RadDatePicker_SetMaxDateToCurrentDate(sender) {
               /* var $MaxDate = new Date();

                sender.set_maxDate(new Date("11/20/2012"));*/
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

            function alertClose(str) {

                var oWnd = radalert(str, 330, 150);
                oWnd.add_close(function () {
                    CloseAndRebind();
                });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
                return false;
            }

            
            //cuando se selecciona un Item del combo
          


            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0) {
                    args.set_enableAjax(false);
                }
                else {
                    args.set_enableAjax(true);
                }
            }

            function AbrirVentana_Excel(Id_Emp, Id_Cd) {
                //debugger;
                var oWnd = radopen("ProPrecioEspecial_Autorizacion_GenExcel.aspx?"
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd                  
                    , "AbrirVentana_vExcel");
                oWnd.center();
            }




        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
       <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                        
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal" >
        <div>
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />

                    <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                        ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
                         
                </Items>
            </telerik:RadToolBar>
            <div style="height: 1px;">
            </div>
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblSucursal2" runat="server" Text="Sucursal" Font-Bold="True"></asp:Label>
                            </td>
                             <td >
                                <asp:Label ID="lblSucursalId" runat="server"></asp:Label>
                                  <asp:Label ID="lblSucursal" runat="server"></asp:Label>
                            </td>
                            <td>
                              
                            </td>
                            <td>
                                &nbsp;
                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSolicitante2" runat="server" Text="Solicitante" Font-Bold="True"></asp:Label>
                            </td>
                             <td>
                                <asp:Label ID="lblSolicitanteId" runat="server"></asp:Label>
                                  <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
                            </td>
                            <td>
                              
                            </td>
                            <td>
                                &nbsp;
                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFolio2" runat="server" Text="Folio" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFolio" runat="server"></asp:Label>
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
                                <asp:Label ID="lblFecSol2" runat="server" Text="Fecha de solicitud" Font-Bold="True"></asp:Label>
                            </td>
                             <td colspan="2"  colspan="2">
                                <asp:Label ID="lblFecSol" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                           
                        </tr>
                        <tr>
                            <td>       
                                <asp:Label ID="lblProveedor" runat="server" Text="Proveedor" Font-Bold="True"></asp:Label>
                             </td>
                           <td>
                                <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="150px" LoadingMessage="Cargando..."
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbProveedor_SelectedIndexChanged" OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td valign="middle" style="margin-left: 40px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbProveedor"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                                <br />
                            </td>
                            <td>
                               &nbsp;
                            </td>
                    </tr>   
                    <tr>         
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Convenio" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNumConvenio" runat="server" Width="70px">                                
                            </telerik:RadTextBox>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Numero de Usuario" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNumUsuario" runat="server" Width="70px">                                   
                            </telerik:RadTextBox>
                        </td>
                            
                    </tr>   
                    <tr>
                        <td>&nbsp;</td>
                    </tr>                 
                    </table>
                    <asp:Label ID="Label3" runat="server" Text="Clientes" Font-Bold="True"></asp:Label>
                    <br />
                    <telerik:RadGrid ID="rgCliente" runat="server" AutoGenerateColumns="False" GridLines="None"
                        MasterTableView-NoMasterRecordsText="No se encontraron registros." OnNeedDataSource="rgCliente_NeedDataSource"
                        PageSize="3" Height="120px" Width="840">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Cte">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblId_Cte" runat="server" Text='<%# Bind("Id_Cte") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                    <HeaderStyle Width="370px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCte_NomComercial" runat="server" Text='<%# Bind("Cte_NomComercial") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="215px" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Nota del solicitante" Font-Bold="True" /><br />
                    <telerik:RadTextBox onpaste="return false" ID="txtNotaSol" runat="server" Rows="3"
                        TextMode="MultiLine" Width="550px" ReadOnly="True">
                    </telerik:RadTextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Nota de respuesta" Font-Bold="True" /><br />
                    <telerik:RadTextBox onpaste="return false" ID="txtNotaResp" runat="server" Rows="3"
                        TextMode="MultiLine" Width="550px">
                    </telerik:RadTextBox>
                    <br />
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
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
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
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
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Modificar todas las vigencias del "></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="100px" DataType="System.DateTime" >
                        <Calendar   runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                TodayButtonCaption="Hoy" />
                        </Calendar>
                      <DateInput runat="server" DateFormat="MMM-yyyy">                                                
                        <ClientEvents OnKeyPress="handleClickEvent" />
                       </DateInput>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                    </telerik:RadDatePicker>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="al "></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dpFecha2" runat="server"   DataType="System.DateTime"  
                    Width="100px">
                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                TodayButtonCaption="Hoy" />
                        </Calendar>
                        <DateInput runat="server" DateFormat="MMM-yyyy">                                                
                            <ClientEvents  />
                        </DateInput>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                    </telerik:RadDatePicker>
                    
                </td>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                        ToolTip="Confirmar" OnClick="ImageButton1_Click" ValidationGroup="ConfirmarFecha" />
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                 
                </td>
                <td>  
                    <telerik:RadGrid ID="RadGrid1" runat="server">
                    </telerik:RadGrid>
                
                    <asp:Panel ID="Panel1" runat="server" Width="1180px" ScrollBars="Horizontal">
                        <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="rg1_NeedDataSource"    OnItemCommand="rg1_ItemCommand"
                            
                            MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                            PageSize="150" Height="600px" Width="1170px">
                        <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="PreciosEspeciales"
                            HideStructureColumns="false" ExportOnlyData="True" Excel-Format="Html"    
                            Excel-FileExtension="xls" >
                        </ExportSettings> 
                            <MasterTableView  Name="Master" EditMode="InPlace" CommandItemDisplay="Top" DataKeyNames="Id_Prd,Prd_Descripcion,Ape_VolVta, Ape_PreVta,Ape_FecInicio,Ape_FecFin,Ape_PreAAA">
                             <ExpandCollapseColumn Visible="True">
                            </ExpandCollapseColumn>
                            <CommandItemSettings ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" 
                              RefreshText="Actualizar"  AddNewRecordText="Nuevo" ></CommandItemSettings>
                               <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Núm. prod." UniqueName="Id_Prd">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Desc. prod." UniqueName="Prd_Descripcion">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Vol. mens." UniqueName="Ape_VolVta">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtVolVta" runat="server" Text='<%# Bind("Ape_VolVta", "{0:N2}") %>'
                                                MaxLength="9" Width="50px" ReadOnly='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus")) == "A" || Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus")) == "R" ? true : false %>'
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Ape_VolVta" HeaderText="Vol. mens." UniqueName="Ape_VolvtaHidden"
                                        DataFormatString="{0:N2}" DataType="System.Double" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Precio" UniqueName="Ape_PreVta">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtPrecioVta" runat="server" Text='<%# Bind("Ape_PreVta", "{0:N2}") %>'
                                                MaxLength="9" Width="50px" 
                                                MinValue="1">                                                
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Ape_PreVta" HeaderText="Precio" UniqueName="Ape_PreVtaHidden"
                                        DataFormatString="{0:N2}" DataType="System.Double" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Mon_Descripcion" HeaderText="Moneda" UniqueName="Mon_Descripcion">
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Inicia" UniqueName="Inicia">
                                        <HeaderStyle Width="110px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>                    
                                        <telerik:RadDatePicker ID="rdp_FecIni" OnSelectedDateChanged="FecIni_SelectedDateChanged" runat="server" DataType="System.DateTime"  
                                                AutoPostBack="true" DbSelectedDate='<%# Bind("Ape_FecInicio") %>'
                                                Width="100px"   Calendar-FirstDayOfWeek="Default" >
                                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                    ViewSelectorText="x">
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy" />
                                                </Calendar>
                                                <DateInput runat="server" DateFormat="MMM-yyyy" >                                                
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </DateInput>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                            </telerik:RadDatePicker>                                
                                            </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Ape_FecInicio" HeaderText="Inicia" UniqueName="IniciaHidden"
                                        DataType="System.DateTime" Visible="False"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn HeaderText="Vigencia" UniqueName="Vigencia">
                                        <HeaderStyle Width="110px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="rdp_FecVigencia" AutoPostBack="true" runat="server" DataType="System.DateTime"  Calendar-FirstDayOfWeek="Default"
                                                DataFormatString="{0:MMM-yyyy}" DbSelectedDate='<%# Bind("Ape_FecFin") %>' Width="100px" OnSelectedDateChanged="FecVigencia_SelectedDateChanged">
                                                <%-- --%>
                                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                    ViewSelectorText="x">
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy" />
                                                </Calendar>
                                                <DateInput runat="server" DateFormat="MMM-yyyy">
                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="RadDatePicker_SetMaxDateToCurrentDate" OnFocus="RadDatePicker_SetMaxDateToCurrentDate"  />
                                                </DateInput>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                 
                                            </telerik:RadDatePicker>
                                            
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                     <telerik:GridBoundColumn DataField="Ape_FecFin" HeaderText="Vigencia" UniqueName="VigenciaHidden"
                                        DataFormatString="{0:MMM-yyyy}" DataType="System.DateTime" Visible="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Ape_PreAAA" HeaderText="Precio AAA" UniqueName="column8"
                                        DataFormatString="{0:N2}" DataType="System.Double">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn HeaderText="Precio AAA Esp." UniqueName="Ape_PreEsp"
                                        EditFormColumnIndex="0">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtPrecioAAAEsp" runat="server" Text='<%# Bind("Ape_PreEsp", "{0:N2}") %>'
                                                MaxLength="9" Width="50px" 
                                                MinValue="1">
                                                
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Ape_PreEsp" HeaderText="Precio AAA Esp" UniqueName="Ape_PreEspHidden"
                                        DataFormatString="{0:N2}" DataType="System.Double" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Autorizar" UniqueName="Autorizar">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkAutorizar" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus")) == "A" ? true : false %>'
                                                GroupName="autoriza" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkAutorizarAll" runat="server" OnCheckedChanged="chkAutorizarAll_CheckedChanged"
                                                GroupName="autorizaAll" AutoPostBack="true" Text="Autorizar" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Rechazar" UniqueName="Rechazado">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkRechazar" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus")) == "R" ? true : false %>'
                                                GroupName="autoriza"/>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkRechazarAll" runat="server" OnCheckedChanged="chkRechazarAll_CheckedChanged"
                                                GroupName="autorizaAll" AutoPostBack="true" Text="Rechazar" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Pendiente" UniqueName="Pendiente">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkPendiente" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus")) == "P" ? true : false %>'
                                                GroupName="autoriza"  />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkPendienteAll" runat="server" OnCheckedChanged="chkPendienteAll_CheckedChanged"
                                                GroupName="autorizaAll" AutoPostBack="true" Text="Pendiente" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                              
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>                                
                                 <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="700px" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_Tipo" runat="server" />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" 
AutoEventWireup="true" CodeBehind="CapGestionRentabilidadSeguimiento.aspx.cs" Inherits="SIANWEB.CapGestionRentabilidadSeguimiento" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // ---------------------
            // Variables de permiso
            // ---------------------


            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }
            function AbrirProyecto(Id_Emp, Id_Cd, Id_Ter, Id_Cte, Cte_NomComercial) {
                var oWnd = radopen("CapGestionRentabilidadSimulador.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Cte=" + Id_Cte
                    + "&Cte_NomComercial=" + Cte_NomComercial);
                oWnd.Maximize();
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToPdfButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
                else {
                    args.set_enableAjax(true);
                }
            }
        </script> 
    </telerik:RadCodeBlock>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">   
           <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFactura">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" 
       >
        <Items>

            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_GridRebind_FacturaPedido" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_GridRebind_FacturaRemisiones" runat="server" Value="0" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <div id="filtros" runat="server">
                        <table border="0">
                            <tr>
                                <td>
                                    Territorio
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtTerritorio" runat="server" Width="30px" MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Representante
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtRepresentante" runat="server" Width="30px" MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td width="110">
                                    Cliente
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="TxtNumeroCliente" runat="server" Width="50px" MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>&nbsp;
                                    <telerik:RadTextBox ID="TxtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Periodo De
                                </td>
                                <td colspan=""2">
                                    <telerik:RadComboBox ID="txtMesInicial" MaxHeight="300px" runat="server" Width="150px">
                                    <Items>
                                          <telerik:RadComboBoxItem Text="Enero" Value="01" />
                                          <telerik:RadComboBoxItem Text="Febrero" Value="02" />
                                          <telerik:RadComboBoxItem Text="Marzo" Value="03"/>
                                          <telerik:RadComboBoxItem Text="Abril" Value="04" />
                                          <telerik:RadComboBoxItem Text="Mayo" Value="05" />
                                          <telerik:RadComboBoxItem Text="Junio" Value="06" />
                                          <telerik:RadComboBoxItem Text="Julio" Value="07" />
                                          <telerik:RadComboBoxItem Text="Agosto" Value="08" />
                                          <telerik:RadComboBoxItem Text="Septiembre" Value="09" />
                                          <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                          <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                          <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                    </Items>
                                    </telerik:RadComboBox>&nbsp;Año&nbsp;
                                    <telerik:RadTextBox ID="TxtAnioInicial" runat="server" Width="30px" MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />                                    
                                    </telerik:RadTextBox>
                                    &nbsp;a:&nbsp;
                                    <telerik:RadComboBox ID="txtMesFinal" MaxHeight="300px" runat="server" Width="150px">
                                    <Items>
                                          <telerik:RadComboBoxItem Text="Enero" Value="01" />
                                          <telerik:RadComboBoxItem Text="Febrero" Value="02" />
                                          <telerik:RadComboBoxItem Text="Marzo" Value="03"/>
                                          <telerik:RadComboBoxItem Text="Abril" Value="04" />
                                          <telerik:RadComboBoxItem Text="Mayo" Value="05" />
                                          <telerik:RadComboBoxItem Text="Junio" Value="06" />
                                          <telerik:RadComboBoxItem Text="Julio" Value="07" />
                                          <telerik:RadComboBoxItem Text="Agosto" Value="08" />
                                          <telerik:RadComboBoxItem Text="Septiembre" Value="09" />
                                          <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                          <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                          <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                    </Items>
                                    </telerik:RadComboBox>
                                    &nbsp;Año&nbsp;<telerik:RadTextBox ID="TxtAnioFinal" runat="server" Width="30px" MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />                                    
                                        </telerik:RadTextBox>
                                </td>
                            </tr>                            
                        </table>
                    </div>


                    <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="1340px" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadGrid ID="rgGestionRentabilidadSeguimiento" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        GridLines="None"
                            PageSize="300" 
                            MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            AllowPaging="True" 
                            AllowSorting="False" 
                            HeaderStyle-HorizontalAlign="Center"
                            DataMember="listNotaCargo"
                            OnNeedDataSource="rgGestionRentabilidadSeguimiento_NeedDataSource" 
                            OnPageIndexChanged="rgGestionRentabilidadSeguimiento_PageIndexChanged"
                            OnItemCommand="rgGestionRentabilidadSeguimiento_ItemCommand" 
                            BorderStyle="None"
                            >

                            <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                                SortToolTip="Clic para reordenar" />

                            <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="ListaNotasCargos"
                                HideStructureColumns="true" ExportOnlyData="true">
                            <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista de notas de cargos" Title="Lista_Notas_Cargos" />
                               </ExportSettings>

                            

                            <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Cte,Id_Ter" ClientDataKeyNames="Id_Cte">

                                 <CommandItemSettings ShowExportToPdfButton="true" ExportToPdfText="Exportar a Pdf"
                        ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" ShowExportToWordButton="true"
                        ExportToWordText="Exportar a Word" ShowExportToCsvButton="false" ExportToCsvText="Exportar a Csv"
                        AddNewRecordText="Agregar"></CommandItemSettings>

                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Núm Terr." UniqueName="Id_Ter">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm Cte." UniqueName="Id_Cte">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="venta" HeaderText="$Venta" UniqueName="venta" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="costo" HeaderText="$Costo" UniqueName="costo" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="UtilidadBruta" HeaderText="$Utilidad Bruta" UniqueName="UtilidadBruta" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>




                                    <telerik:GridBoundColumn DataField="ventap" HeaderText="$Venta" UniqueName="ventap" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="costop" HeaderText="$Costo" UniqueName="costop" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="UtilidadBrutap" HeaderText="$Utilidad Bruta" UniqueName="UtilidadBrutap" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="CrearProyecto" HeaderText="Crear Proyecto" UniqueName="CrearProyecto">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>                                    

                        
                                    
                                                                  
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                
                            </ClientSettings>
                        </telerik:RadGrid>



                    <div id="DivTotales" runat="server">
                        <table border="0">
                            <tr>
                                <td width="590px" align="right">
                                    Totales
                                </td>
                                <td width="90px">
                                    <telerik:RadNumericTextBox ID="Totventa" runat="server" Width="90px" MaxLength="9"
                                        CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                    </asp:Panel>
                    
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
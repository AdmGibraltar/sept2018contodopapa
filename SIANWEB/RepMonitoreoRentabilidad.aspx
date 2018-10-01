<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepMonitoreoRentabilidad.aspx.cs" Inherits="SIANWEB.RepMonitoreoRentabilidad" %>

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

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
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
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtRepresentante" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

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
                                <td width="110">
                                    Cliente
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>                                    
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Territorio
                                </td>
                                <td>                                    
                                    <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="5" onpaste="return false">                                        
                                        <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>                                                     
                                </td>                    
                                <td>
                                    <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            EnableLoadOnDemand="True" Filter="Contains" 
                                                            OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbTer_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Representante
                                </td>
                                <td>
                                    <telerik:radtextbox id="txtRepresentante" runat="server" AutoPostBack="true" maxlength="5"
                                            onpaste="return false" OnTextChanged="txtRep_TextChanged">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:radtextbox>
                                    <asp:HiddenField runat="server" id="txtRepOld"/>                                                                            
                                </td>
                                <td>
                                   <telerik:radtextbox id="txtRepresentanteStr" runat="server" width="300px" maxlength="5"
                                                onpaste="return false" enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:radtextbox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    Periodo De
                                </td>
                                <td colspan="2">
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
                                        </telerik:RadTextBox>&nbsp;
                                        <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="btnBuscar_Click"  />
                                </td>
                            </tr>
                        </table>
                    </div>


                                                                <table style="empty-cells: show; table-layout: auto" id="ctl00_CPH_rgAcuerdo_ctl00"
                                                                    class="rgMasterTable" cellspacing="0" border="1">
                                                                        <tr>
                                                                            <th style="width: 400px; text-align: center; background-color: #EAEAEA" class="rgHeader" scope="col" >
                                                                                &nbsp;
                                                                            </th>
                                                                            <th style="width: 465px; text-align: center; background-color: #EAEAEA" class="rgHeader" scope="col" >
                                                                                Situación Actual
                                                                            </th>
                                                                            <th style="width: 465px; text-align: center; background-color: #EAEAEA" class="rgHeader" scope="col"
                                                                                >
                                                                                Planteamiento
                                                                            </th>
                                                                        </tr>
                                                                 </table>
                    <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="1340px" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadGrid ID="rgGestionRentabilidad" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        GridLines="None"
                            PageSize="300" 
                            MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            AllowPaging="True" 
                            AllowSorting="False" 
                            HeaderStyle-HorizontalAlign="Center"
                            DataMember="listNotaCargo"
                            OnNeedDataSource="rgGestionRentabilidad_NeedDataSource" 
                            OnPageIndexChanged="rgGestionRentabilidad_PageIndexChanged"
                            OnItemCommand="rgGestionRentabilidad_ItemCommand" 
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


                                    <telerik:GridBoundColumn DataField="Costo" HeaderText="$Costo" UniqueName="Costo" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="UtilidadBruta" HeaderText="$UB" UniqueName="UtilidadBruta" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="InversionSP" HeaderText="$Inversión SP" UniqueName="InversionSP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  



                                    <telerik:GridBoundColumn DataField="InversionCT" HeaderText="$Inversión CT" UniqueName="InversionCT" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  

                                    <telerik:GridBoundColumn DataField="PorcURem" HeaderText="%UT Rem" UniqueName="PorcURem" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="ComisionRIK" HeaderText="$COMISION RIK" UniqueName="InversionCT" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  
                        


                                    <telerik:GridBoundColumn DataField="ventaP" HeaderText="$Venta" UniqueName="ventaP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="CostoP" HeaderText="$Costo" UniqueName="CostoP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="UtilidadBrutaP" HeaderText="$UB" UniqueName="UtilidadBrutaP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="InversionSPP" HeaderText="$Inversión SP" UniqueName="InversionSPP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  



                                    <telerik:GridBoundColumn DataField="InversionCTP" HeaderText="$Inversión CT" UniqueName="InversionCTP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  

                                    <telerik:GridBoundColumn DataField="PorcURemP" HeaderText="%UT Rem" UniqueName="PorcURemP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>  


                                    <telerik:GridBoundColumn DataField="ComisionRIKP" HeaderText="$COMISION RIK" UniqueName="ComisionRIKP" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
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




                    </asp:Panel>                    
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
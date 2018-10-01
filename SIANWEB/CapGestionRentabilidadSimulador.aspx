<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CapGestionRentabilidadSimulador.aspx.cs"
    Inherits="SIANWEB.CapGestionRentabilidadSimulador" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // ---------------------
            // Variables de permiso
            // ---------------------
            function ToolBar_ClientClick(sender, args) {

                //Aqui hay que validar si todo esta bien capturado

                var urlArchivo  = 'ObtenerEstausGestionRentabilidad.aspx';
                var Id_Ter      =  $find('<%= txtTerritorio.ClientID %>');
                var Id_Cte      =  $find('<%= TxtNumeroCliente.ClientID %>');
                var mesInicial  = $find('<%= txtMesInicial.ClientID %>');
                var anioInicial = $find('<%= TxtAnioInicial.ClientID %>');
                var mesFinal    = $find('<%= txtMesFinal.ClientID %>');
                var anioFinal =  $find('<%= TxtAnioFinal.ClientID %>');
                var DondeViene = "<%= Convert.ToString(Request.QueryString["txtDondeViene"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["txtDondeViene"]) %>";


                parametros = "Id_Ter=" + Id_Ter.get_value();
                parametros = parametros + "&Id_Cte=" + Id_Cte.get_value();
                parametros = parametros + "&mesInicial=" + mesInicial.get_value();
                parametros = parametros + "&anioInicial=" + anioInicial.get_value();
                parametros = parametros + "&mesFinal=" + mesFinal.get_value();
                parametros = parametros + "&anioFinal=" + anioFinal.get_value();

                if (obtenerrequest(urlArchivo, parametros) == "S") {
                    alert("Favor de terminar de capturar las Acciones con sus periodos.");
                }
                else {
                    if (DondeViene != "0") {
                        document.location.href = "RepMonitoreoIndicadores.aspx?Id_Rik=" + DondeViene + "&TxtAnioInicial=" + anioInicial.get_value()  + "&Id_Ter=" + Id_Ter.get_value() + "&TxtAnioFinal=" + anioFinal.get_value() + "&txtMesInicial=" + mesInicial.get_value() + "&txtMesFinal=" + mesFinal.get_value();
                    } else {
                        document.location.href = "CapGestionRentabilidad.aspx?TxtAnioInicial=" + anioInicial.get_value() 
                                                                          + "&TxtAnioFinal=" + anioFinal.get_value() 
                                                                          + "&txtMesInicial=" + mesInicial.get_value() 
                                                                          + "&txtMesFinal=" + mesFinal.get_value()
                                                                          + "&StxtTerritorio=" + "<%=Convert.ToString(Request.QueryString["StxtTerritorio"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtTerritorio"])%>"
                                                                          + "&StxtRepresentante=" + "<%=Convert.ToString(Request.QueryString["StxtRepresentante"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtRepresentante"])%>"
                                                                          + "&STxtNumeroCliente=" + "<%=Convert.ToString(Request.QueryString["STxtNumeroCliente"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["STxtNumeroCliente"])%>"
                                                                          + "&STxtPorCliente=" + "<%=Convert.ToString(Request.QueryString["STxtPorCliente"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["STxtPorCliente"])%>" 
                                                                          + "&STxtPorQuimicos=" + "<%=Convert.ToString(Request.QueryString["STxtPorQuimicos"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["STxtPorQuimicos"])%>" 
                                                                          + "&STxtPorPapelTradicional=" + "<%=Convert.ToString(Request.QueryString["STxtPorPapelTradicional"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["STxtPorPapelTradicional"])%>" 
                                                                          + "&STxtPorSistemaDiferenciado=" + "<%=Convert.ToString(Request.QueryString["STxtPorSistemaDiferenciado"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["STxtPorSistemaDiferenciado"])%>"
                                                                          + "&StxtPorJarcieria=" + "<%=Convert.ToString(Request.QueryString["StxtPorJarcieria"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtPorJarcieria"])%>" 
                                                                          + "&StxtPorAccesorios=" + "<%=Convert.ToString(Request.QueryString["StxtPorAccesorios"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtPorAccesorios"])%>"
                                                                          + "&StxtPorBolsaBasura=" + "<%=Convert.ToString(Request.QueryString["StxtPorBolsaBasura"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtPorBolsaBasura"])%>"
                                                                          + "&StxtCategorias=" + "<%=Convert.ToString(Request.QueryString["StxtCategorias"]) == string.Empty ? "" : Convert.ToString(Request.QueryString["StxtCategorias"])%>";

                    }
                }

            }
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
            function ClientTabSelecting(sender, args) {

            }
            function onResize(sender, eventArgs) {

            }

   
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGestionRentabilidadSimulador" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight=""/>
                </UpdatedControls>
            </telerik:AjaxSetting>



            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGestionRentabilidadResultados" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SA_Ventas_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SA_Costo_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SA_UB_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SA_UB_Porcentaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SA_ComisionRIK_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>



            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SP_Ventas_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SP_Costo_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SP_UB_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>



            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SP_UB_Porcentaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SP_ComisionRIK_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>



            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="M_AhorroMensualClientes_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="M_AhorroMensualClientes_Porcentaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="M_MejoraUB_Pesos" />
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="rgGestionRentabilidadSimulador">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="M_MejoraUB_Porcentaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

 <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"  OnClientButtonClicking="ToolBar_ClientClick"
       >
        <Items>

                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="printAcciones" ToolTip="Imprimir Acciones" CssClass="Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                                            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" Runat="server"/>
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
                                    <telerik:RadTextBox ID="txtTerritorio" runat="server" Width="30px" MaxLength="5"
                                        onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>


                                </td>
                            </tr>
                            <tr>
                                <td width="110">
                                    Cliente
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="TxtNumeroCliente" runat="server" Width="50px" MaxLength="5"
                                        onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>&nbsp;
                                    <telerik:RadTextBox ID="TxtNombreCliente" runat="server" Width="300px" MaxLength="100"
                                        onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
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
                                            <telerik:RadComboBoxItem Text="Marzo" Value="03" />
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
                                    &nbsp;Año&nbsp;
                                    <telerik:RadTextBox ID="TxtAnioInicial" runat="server" Width="30px" MaxLength="5"
                                        onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                    &nbsp;a:&nbsp;
                                    <telerik:RadComboBox ID="txtMesFinal" MaxHeight="300px" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Enero" Value="01" />
                                            <telerik:RadComboBoxItem Text="Febrero" Value="02" />
                                            <telerik:RadComboBoxItem Text="Marzo" Value="03" />
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
                                    &nbsp;Año&nbsp;<telerik:RadTextBox ID="TxtAnioFinal" runat="server" Width="30px"
                                        MaxLength="5" onpaste="return false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Simulador" AccessKey="S" PageViewID="RadPageViewSimulador"
                                Value="Simulador">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Resultados" AccessKey="R" PageViewID="RadPageViewResultados"
                                Value="Resultados">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Acciones" AccessKey="A" PageViewID="RadPageViewAcciones"
                                Value="Acciones">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" Width="2340px" Height="1000px">
                        <telerik:RadPageView ID="RadPageViewSimulador" runat="server" Width="2340px" Height="1000px">
                            <%--

                    <telerik:RadSplitter ID="RadSplitter2" runat="server"  ResizeMode="EndPane"
                                 BorderSize="0" Width="2340px" Height="1000px" >
                                <telerik:RadPane ID="RadPane2" runat="server" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="0px"   Width="2340px" Height="1000px">

                            --%>
                            <%--<asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="1340px" BorderStyle="Solid"
                        BorderWidth="1px">--%>
                            <telerik:RadGrid ID="rgGestionRentabilidadSimulador" runat="server" GridLines="None"
                                AllowPaging="false" AutoGenerateColumns="False" OnNeedDataSource="rgGestionRentabilidadSimulador_NeedDataSource"
                                OnInsertCommand="rgGestionRentabilidadSimulador_InsertCommand" OnUpdateCommand="rgGestionRentabilidadSimulador_UpdateCommand"
                                OnDeleteCommand="rgGestionRentabilidadSimulador_DeleteCommand" OnItemDataBound="rgGestionRentabilidadSimulador_ItemDataBound"
                                OnItemCommand="rgGestionRentabilidadSimulador_ItemCommand" OnPageIndexChanged="rgGestionRentabilidadSimulador_PageIndexChanged"
                                BorderStyle="None" OnItemCreated="rgGestionRentabilidadSimulador_ItemCreated"
                                GroupingSettings-RetainGroupFootersVisibility="true" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true" Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Emp,Id_Cd,Id_Cte,Cpr_Descripcion,Id_Prd"
                                    EditMode="InPlace" HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros."
                                    PageSize="300">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm Cte." UniqueName="Id_Cte"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                            UniqueName="DeleteColumn">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn HeaderText="Categoria" UniqueName="Cpr_Descripcion" DataField="Cpr_Descripcion"
                                            GroupByExpression="Cpr_Descripcion Group by Cpr_Descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCpr_Descripcion" runat="server" Text='<%# Bind("Cpr_Descripcion") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Núm Prod." UniqueName="Id_Prd">
                                            <ItemTemplate>
                                                <asp:Label ID="NEId_Prd" runat="server" Text='<%# Bind("Id_Prd") %>' Width="30px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NId_PrdP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%# Bind("Id_Prd") %>' ReadOnly="true">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="Prd_Descripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox Width="200px" ID="NPrd_DescripcionP" runat="server" ReadOnly="true"
                                                    Text='<%# Bind("Prd_Descripcion") %>'>
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Consumo" UniqueName="cantidad" FooterText="Total : "
                                            Aggregate="Sum" DataField="cantidad">
                                            <ItemTemplate>
                                                <asp:Label ID="cantidad" runat="server" Text='<%#String.Format("{0:n2}",Eval("cantidad")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="Ncantidad" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("cantidad")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="10px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Precio Venta" UniqueName="PrecioVenta">
                                            <ItemTemplate>
                                                <asp:Label ID="PrecioVenta" runat="server" Text='<%#String.Format("{0:n2}",Eval("PrecioVenta")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NPrecioVenta" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("PrecioVenta")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Precio AAA" UniqueName="PrecioDistribuidor">
                                            <ItemTemplate>
                                                <asp:Label ID="PrecioDistribuidor" runat="server" Text='<%#String.Format("{0:n2}",Eval("PrecioDistribuidor")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NPrecioDistribuidor" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("PrecioDistribuidor")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Venta" UniqueName="venta" FooterText=":"
                                            Aggregate="Sum" DataField="venta" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="venta" runat="server" Text='<%#String.Format("{0:n2}",Eval("venta")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="Nventa" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("venta")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Costo" UniqueName="Costo" FooterText=":"
                                            Aggregate="Sum" DataField="Costo" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="Costo" runat="server" Text='<%#String.Format("{0:n2}",Eval("Costo")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NCosto" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("Costo")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$UB" UniqueName="UtilidadBruta" FooterText=":"
                                            Aggregate="Sum" DataField="UtilidadBruta" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="UtilidadBruta" runat="server" Text='<%#String.Format("{0:n2}",Eval("UtilidadBruta")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NUtilidadBruta" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%#String.Format("{0:n2}",Eval("UtilidadBruta")) %>'
                                                    ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="%UB" UniqueName="PorcUBReal">
                                            <ItemTemplate>
                                                <asp:Label ID="PorcUBReal" runat="server" Text='<%# Bind("PorcUBReal") %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="NPorcUBReal" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" Text='<%# Bind("PorcUBReal") %>' ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Accion" UniqueName="Accion">
                                            <ItemTemplate>
                                                <asp:Label ID="Accion" runat="server" Text='<%# Bind("Accion") %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="ComboAccion" runat="server" Width="100px" SelectedValue='<%# Bind("Accion") %>'
                                                    AutoPostBack="true" OnSelectedIndexChanged="cmbAccion_SelectedIndexChanged">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Cambio de Producto" Value="Cambio de Producto" />
                                                        <telerik:RadComboBoxItem Text="Incremento en Precio" Value="Incremento en Precio" />
                                                        <telerik:RadComboBoxItem Text="Producto Adicional" Value="Producto Adicional" />
                                                        <telerik:RadComboBoxItem Text="Disminuir Precio" Value="Disminuir Precio" />
                                                        <telerik:RadComboBoxItem Text="Disminuir Consumo" Value="Disminuir Consumo" />
                                                        <telerik:RadComboBoxItem Text="Cancelar Consumo" Value="Cancelar Consumo" />
                                                        <telerik:RadComboBoxItem Text="Remplazar Equipo de Base Instalada" Value="Remplazar Equipo de Base Instalada" />
                                                        <telerik:RadComboBoxItem Text="Sin Acción" Value="Sin Acción" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Núm Prod." UniqueName="Id_PrdP">
                                            <ItemTemplate>
                                                <asp:Label ID="LbId_PrdP" runat="server" Text='<%# Bind("Id_PrdP") %>' Width="30px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EId_PrdP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" OnTextChanged="txtProducto_TextChanged">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_DescripcionP">
                                            <ItemTemplate>
                                                <asp:Label ID="Prd_DescripcionP" runat="server" Text='<%# Bind("Prd_DescripcionP") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox Width="200px" ID="EPrd_DescripcionP" runat="server" ReadOnly="true" >
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Consumo" UniqueName="cantidadP" FooterText="Total:"
                                            Aggregate="Sum" DataField="cantidadP">
                                                    <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="cantidadP" runat="server" Text='<%# Bind("cantidadP") %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EcantidadP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" OnTextChanged="txtProductoCantidad_TextChanged">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="10px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Precio Venta" UniqueName="PrecioVentaP">
                                            <ItemTemplate>
                                                <asp:Label ID="PrecioVentaP" runat="server" Text='<%#String.Format("{0:n2}", Eval("PrecioVentaP"))%>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EPrecioVentaP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" OnTextChanged="txtProductoPrecio_TextChanged">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Precio AAA" UniqueName="PrecioDistribuidorP">
                                            <ItemTemplate>
                                                <asp:Label ID="PrecioDistribuidorP" runat="server" Text='<%#String.Format("{0:n2}", Eval("PrecioDistribuidorP")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EPrecioDistribuidorP" runat="server" Width="50px"
                                                    MaxLength="9" MinValue="1" AutoPostBack="true" ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Venta" UniqueName="ventaP" FooterText=":"
                                            Aggregate="Sum" DataField="ventaP" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="ventaP" runat="server" Text='<%#String.Format("{0:n2}", Eval("ventaP")) %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EventaP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$Costo" UniqueName="CostoP" FooterText=":"
                                            Aggregate="Sum" DataField="CostoP" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="CostoP" runat="server" Text='<%#String.Format("{0:n2}", Eval("CostoP")) %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="ECostoP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="$UB" UniqueName="UtilidadBrutaP" FooterText=":"
                                            Aggregate="Sum" DataField="UtilidadBrutaP" FooterAggregateFormatString="{0:N2}">
                                            <ItemTemplate>
                                                <asp:Label ID="UtilidadBrutaP" runat="server" Text='<%#String.Format("{0:n2}", Eval("UtilidadBrutaP")) %>'
                                                    DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EUtilidadBrutaP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="%UB" UniqueName="PorcUBRealP">
                                            <ItemTemplate>
                                                <asp:Label ID="PorcUBRealP" runat="server" Text='<%#String.Format("{0:n2}", Eval("PorcUBRealP")) %>' DataFormatString="{0:N2}" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="EPorcUBRealP" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="1" AutoPostBack="true" ReadOnly="true">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Categoria" FieldName="Cpr_Descripcion" HeaderValueSeparator=": ">
                                                </telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Cpr_Descripcion" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                </MasterTableView>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                    LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                    ShowPagerText="True" PageButtonCount="3" />
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            <%--</asp:Panel>--%>
                            </telerik:RadPane>
                            <%--  </telerik:RadSplitter> --%>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewResultados" runat="server" Width="2340px" Height="1000px">
                            <telerik:RadSplitter ID="RadSplitter3" runat="server" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true"
                                BorderSize="0" Width="2340px" Height="1000px">
                                <telerik:RadPane ID="RadPane3" runat="server" OnClientResized="onResize" BorderColor="White"
                                    BorderStyle="Solid" BorderWidth="1px" Width="2340px" Height="1000px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div id="CPH_ctl00$CPH$pnlGridPanel">
                                                        <div style="width: 1600px" id="CPH_pnlGrid">
                                                            <div style="width: 1600px" id="ctl00_CPH_rgAcuerdo" class="RadGrid RadGrid_Outlook">
                                                                <table style="width: 1600px; empty-cells: show; table-layout: auto" id="ctl00_CPH_rgAcuerdo_ctl00"
                                                                    class="rgMasterTable" cellspacing="0">
                                                                    <colgroup>
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                        <col style="width: 5px">
                                                                    </colgroup>
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="text-align: center" class="rgHeader" scope="col" colspan="8">
                                                                                Situación Actual
                                                                            </th>
                                                                            <th style="text-align: center; background-color: #EAEAEA" class="rgHeader" scope="col"
                                                                                colspan="8">
                                                                                Planteamiento
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Venta $
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Costo AAA $
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UB$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UB%
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Inversión SP$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Inversión CT$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UT REM$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                COMISION RIK$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Venta $
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Costo AAA $
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UB$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UB%
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Inversión SP$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                Inversión CT$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                UT REM$
                                                                            </th>
                                                                            <th style="text-align: center" class="rgHeader" scope="col">
                                                                                COMISION RIK$
                                                                            </th>
                                                                            <tbody>
                                                                                <tr id="ctl00_CPH_rgAcuerdo_ctl00__0" class="rgRow">
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_Ventas_Pesos" runat="server" Width="80px" 
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_Costo_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_UB_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_UB_Porcentaje" runat="server" Width="80px" 
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_InversionSP_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_InversionCT_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_UtRem_Pesos" runat="server" Width="80px" 
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SA_ComisionRIK_Pesos" runat="server" Width="80px" 
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_Ventas_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_Costo_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_UB_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_UB_Porcentaje" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_InversionSP_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_InversionCT_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_UtRem_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                    <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="SP_ComisionRIK_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div id="CPH_ctl00$CPH$pnlGridPanel">
                                                            <div style="width: 1400px" id="CPH_pnlGrid">
                                                                <div style="width: 1400px" id="ctl00_CPH_rgAcuerdo" class="RadGrid RadGrid_Outlook">
                                                                    <table style="width: 1400px; empty-cells: show; table-layout: auto" id="ctl00_CPH_rgAcuerdo_ctl00"
                                                                        class="rgMasterTable" cellspacing="0">
                                                                        <colgroup>
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                            <col style="width: 5px">
                                                                        </colgroup>
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="text-align: center" class="rgHeader" scope="col" colspan="4">
                                                                                    Mejoras
                                                                                </th>
                                                                                <th style="text-align: center; background-color: #EAEAEA" class="rgHeader" scope="col"
                                                                                    colspan="3">
                                                                                    Acciones VPN
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    Ahorro Mensual en Cliente $
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    Ahorro Mensual en Cliente %
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    Mejora en UB$
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    Mejora en UB%
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    UAFIR Incremental$
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    Inversión Incremental$
                                                                                </th>
                                                                                <th style="text-align: center" class="rgHeader" scope="col">
                                                                                    VPN$
                                                                                </th>
                                                                                <tbody>
                                                                                    <tr id="ctl00_CPH_rgAcuerdo_ctl00__0" class="rgRow">
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="M_AhorroMensualClientes_Pesos" runat="server" Width="80px"
                                                                                            Value="0"  Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="M_AhorroMensualClientes_Porcentaje" runat="server" Width="80px" 
                                                                                            Value="0"  Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="M_MejoraUB_Pesos" runat="server" Width="80px"
                                                                                            Value="0"  Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="M_MejoraUB_Porcentaje" runat="server" Width="80px"
                                                                                            Value="0"  Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="AVPN_UAFIRIncremental_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="AVPN_InversionIncremental_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                        <td align="middle">
                                                                                        <telerik:RadNumericTextBox ID="AVPN_VPN_Pesos" runat="server" Width="80px"
                                                                                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                        </telerik:RadNumericTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                    </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewAcciones" runat="server" Width="840px" Height="1000px">
                        <telerik:RadGrid 
                            ID="rgGestionRentabilidadResultados" 
                            runat="server" 
                            GridLines="None"
                            AllowPaging="false" 
                            AutoGenerateColumns="False" 
                            OnNeedDataSource="rgGestionRentabilidadResultados_NeedDataSource" 
                            OnInsertCommand="rgGestionRentabilidadResultados_InsertCommand"
                            OnUpdateCommand="rgGestionRentabilidadResultados_UpdateCommand" 
                            OnDeleteCommand="rgGestionRentabilidadResultados_DeleteCommand"
                            OnItemDataBound="rgGestionRentabilidadResultados_ItemDataBound" 
                            OnItemCommand="rgGestionRentabilidadResultados_ItemCommand" 
                            OnPageIndexChanged="rgGestionRentabilidadResultados_PageIndexChanged"
                            BorderStyle="None" 
                            OnItemCreated="rgGestionRentabilidadResultados_ItemCreated"
                            GroupingSettings-RetainGroupFootersVisibility="true"
                            ShowFooter="True">
                            <MasterTableView 
                                ShowGroupFooter="true"
                                Name="Master" 
                                CommandItemDisplay="Top"
                                DataKeyNames="Id_Emp,Id_Cd,Id_Cte,Cpr_Descripcion,Id_Prd" 
                                EditMode="InPlace" 
                                HorizontalAlign="NotSet" 
                                AutoGenerateColumns="False"
                                NoMasterRecordsText="No se encontraron registros." 
                            PageSize="300" >                                
                              
                               <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm Cte." UniqueName="Id_Cte" Display="false">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </telerik:GridEditCommandColumn>



                                  <telerik:GridTemplateColumn HeaderText="Accion" UniqueName="Accion" DataField="Accion" GroupByExpression="Accion Group by Accion">
                                        <ItemTemplate>
                                            <asp:Label ID="Accion" runat="server" Text='<%# Bind("Accion") %>' DataFormatString="{0:N2}"/>
                                        </ItemTemplate>
					                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="PAccion" runat="server" Width="70px" MaxLength="9"
                                                            MinValue="1" 
                                                            AutoPostBack="true" Text='<%# Bind("Accion") %>' ReadOnly="true">
                                                        </telerik:RadTextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />					                    
                                     </telerik:GridTemplateColumn>
  



                                    <telerik:GridTemplateColumn HeaderText="Núm Prod." UniqueName="Id_Prd">
                                        <ItemTemplate>
                                            <asp:Label ID="NEId_Prdx" runat="server" Text='<%# Bind("Id_Prd") %>' Width="30px"/>
                                        </ItemTemplate>
					                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="NEId_Prdx" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" 
                                                            AutoPostBack="true" Text='<%# Bind("Id_Prd") %>' ReadOnly="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                     </telerik:GridTemplateColumn>
                                                                       

                                    <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Descripcion" >
                                        <ItemTemplate>
                                            <asp:Label ID="Prd_Descripcion" runat="server" Text='<%# Bind("Prd_Descripcion") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                                        <telerik:RadTextBox Width="200px" ID="NPrd_DescripcionPx" runat="server" ReadOnly="true" Text='<%# Bind("Prd_Descripcion") %>'></telerik:RadTextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                     </telerik:GridTemplateColumn>
                                    
                                    


                                        <telerik:GridTemplateColumn HeaderText="Mes" UniqueName="Mes">
                                            <ItemTemplate>
                                                <asp:Label ID="MesAccion" runat="server"  Width="30px" Text='<%# Bind("MesAccionNombre") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="MesAccionP" runat="server" Width="100px" SelectedValue='<%# Bind("MesAccionNumero") %>'
                                                    AutoPostBack="true" >
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Enero" Value="01" />
                                                        <telerik:RadComboBoxItem Text="Febrero" Value="02" />
                                                        <telerik:RadComboBoxItem Text="Marzo" Value="03" />
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
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                                                 

                                    <telerik:GridTemplateColumn HeaderText="Año" UniqueName="Anio">
                                        <ItemTemplate>
                                            <asp:Label ID="AnioAccion" runat="server"  Width="30px" Text='<%# Bind("AnioAccion") %>'/>
                                        </ItemTemplate> 
					                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="AnioAccionP" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" 
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                     </telerik:GridTemplateColumn>
                                </Columns>
                            <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Acción" FieldName="Accion" 
                                        HeaderValueSeparator=": "></telerik:GridGroupByField>                                        
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Accion" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                           </GroupByExpressions>

                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

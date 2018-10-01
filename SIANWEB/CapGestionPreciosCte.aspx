<%@ Page Title="Vincular cliente" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosCte.aspx.cs" Inherits="SIANWEB.CapGestionPreciosCte" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

         

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }


            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
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
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
                        }

            function TabSelected(sender, args) {

            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function refreshGrid() {

            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
              
                 <telerik:RadToolBarButton CommandName="guardar" Value="guardar" ToolTip="Guardar" CssClass="save"  ValidationGroup="guardar"
                ImageUrl="~/Imagenes/blank.png" />
               
            </Items>
        </telerik:RadToolBar>
        <br />
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
        <table style="font-family: Verdana; font-size: 7pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                     <tr>
                            <td>
                           
                            
                           
                                &nbsp;</td>
                            <td>
                       
                                &nbsp;</td>
                            <td>
                              
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            <td>
                                &nbsp;</td>
                               <td>
                                   &nbsp;</td>
                            <td >
                       
                            </td>
                        </tr>


                      <tr>
                            <td class="style1">
                           
                            
                           
                                &nbsp;</td>
                            <td class="style1">
                                 &nbsp;</td>
                            <td class="style1">
                                  &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
                            
                            <td class="style1">
                               <asp:Label ID="Label5" Text="Convenio" runat="server">
                                </asp:Label>
                              </td>
                  
                              <td  >
                              <asp:Label runat ="Server" ID="LblPC_NoConvenio" ></asp:Label>
                                  &nbsp;</td>
                        </tr>
                                <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                   &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" Text="Nombre" runat="server">
                                </asp:Label>
                                    </td>
                                  <td  width="50%">
                                      <asp:Label runat ="Server" ID="LblPc_Nombre" ></asp:Label></td>
                        </tr>
                          <tr>
                            <td >
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                           
                            
                           
                                &nbsp;</td>
                            <td>
                           
                            
                           
                               <asp:Label ID="Label4" Text="Categoría" runat="server">
                                </asp:Label>
                           
                              </td>
                              <td >
                                   <asp:Label runat ="Server" ID="LblId_CatStr"></asp:Label></td>
                        </tr>
                               <tr>
                            <td  >
                                &nbsp;</td>
                          <td>
                                  &nbsp;</td>
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
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                              <td >
                       
                            </td>
                        </tr>
                        <tr>
                        <td colspan ="4">
                            &nbsp;</td>
                        <td colspan="2">
                    
                        </td>
                        </tr>
                        <tr>
                        <td colspan = "6">
                            
                  
                    <br />
                       <table>
                         <tr>
                            <td>
                               <telerik:RadGrid ID="rgConvenioDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                 EnableLinqExpressions="False"  autopostback="True"
                                PageSize="15"  width="1000px"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               >
                                <MasterTableView ClientDataKeyNames="Id_Prd">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                         
                                    <Columns>

                                      <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Clave Key" UniqueName="Id_Prd" >
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="PCD_ClaveProv" HeaderText="Clave Proveedor" UniqueName="PC_NoConvenio">
                                            <HeaderStyle Width="80px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripción">
                                            <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_PrecioVtaMin" HeaderText="P. Venta Min." UniqueName="PCD_PrecioVtaMin">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_PrecioVtaMax" HeaderText="P. Venta Max." UniqueName="PCD_PrecioVtaMax">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="PCD_CantidadMax" HeaderText="Cantidad max." UniqueName="PCD_CantidadMax">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Id_Moneda" HeaderText="Id_Moneda" UniqueName="Id_Moneda" visible ="False">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_MonedaStr" HeaderText="Moneda" UniqueName="Id_MonedaStr" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="PCD_CatDesp" HeaderText="Cat. Despachador" UniqueName="PCD_CatDesp" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn DataField="PCD_PrecioAAAEsp" HeaderText="<b>Anterior</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEsp" visible = "false"
                                               DataFormatString="{0:N2}" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicio" HeaderText="<b>Anterior</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicio"  visible = "false"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspA" HeaderText="<b>Anterior</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspA" 
                                           DataFormatString="{0:N2}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicioA" HeaderText="<b>Anterior</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioA"  
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspB" HeaderText="<b>Actual</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspB"  
                                                DataFormatString="{0:N2}" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicioB" HeaderText="<b>Actual</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioB" 
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PCD_PrecioAAAEspC" HeaderText="<b>Futuro</b> <br> Precio AAA. Esp." UniqueName="PCD_PrecioAAEspC"  
                                                DataFormatString="{0:N2}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PCD_FechaInicioC" HeaderText="<b>Futuro</b> <br> Fecha inicio"  UniqueName="PCD_FechaInicioC"  
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="PCD_FechaFin" HeaderText="Fecha fin"  UniqueName="PCD_FechaFin"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                            </td>
                       
                        </tr>
                    </table>
             
                        </td>
                        </tr>
                    </table>
                 
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                    <asp:HiddenField ID="HFCat_Consecutivo" runat="server" />
                    <asp:HiddenField ID="HFId_PC" runat="server" />
                    <asp:HiddenField ID="HFTipoOp" runat="server" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
    
    </style>
</asp:Content>


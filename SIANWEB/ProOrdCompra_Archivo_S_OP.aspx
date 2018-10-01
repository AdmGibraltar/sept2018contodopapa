<%@ Page Title="Generación de órdenes S&OC" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProOrdCompra_Archivo_S_OP.aspx.cs" Inherits="SIANWEB.ProOrdCompra_Archivo_S_OP" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">


        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                //--------------------------------------------------------------------------------------------------
                //Abre la ventana para subir archivos de Excel
                //--------------------------------------------------------------------------------------------------
                function AbrirVentana_Excel(Id_Emp, Id_Cd, proveedor, productoInicial, productoFinal, aplicaTransito) {
                    //debugger;
                    var oWnd = radopen("ProOrdenCompra_Archivo_S_OP_Excel.aspx?"
                        + "&Id_Emp=" + Id_Emp
                        + "&Id_Cd=" + Id_Cd
                          + "&proveedor=" + proveedor
                        , "AbrirVentana_vExcel");
                    oWnd.center();
                }


                function LimpiarBanderaRebind(sender, eventArgs) {
                }
                function ActivarBanderaRebind_Excel() {
                }

                //cuando se selecciona un Item del combo
                function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedor.ClientID %>'));
                }

                //cuando el campo de texto pirde el foco
                function txtProveedor_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbProveedor.ClientID %>'));
                }

            </script>
        </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">

        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>

                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick" >
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />

            <telerik:RadToolBarButton CommandName="subirExcel" Value="subirExcel" Text="" CssClass="descExcel"
                ToolTip="Subir archivo Excel" ImageUrl="Imagenes/blank.png" />         
            
             <telerik:RadToolBarButton CommandName="bajarExcel" Value="bajarExcel" CssClass="Excel" ToolTip="Exportar a Excel"
                ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />   
        </Items>
    </telerik:RadToolBar>


    <div class="formulario" id="divPrincipal" runat="server">
    <br />

        <table>
             <tr>
                    <td>
                        <asp:Label ID="lblProveedor" runat="server" Text="Proveedor"></asp:Label>
                    </td>
                    <td>
                                <telerik:RadNumericTextBox ID="txtProveedor" runat="server" MaxLength="9" MinValue="1"
                                    Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                             <ClientEvents OnBlur="txtProveedor_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                    </td>
                    <td>
                                    <telerik:RadComboBox ID="cmbProveedor" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                        LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                        OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged" Width="300px"
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
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtProveedor" runat="server" ControlToValidate="txtProveedor"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar">
                                    </asp:RequiredFieldValidator>
                                </td>
            </tr>
        </table>


          <telerik:RadGrid  RenderMode="Lightweight" ID="grid" runat="server"  AllowPaging="true"
            PageSize="7" AutoGenerateColumns="false">
             
             <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="OrdenCompra"
                            HideStructureColumns="true" ExportOnlyData="True" 
                            Excel-FileExtension="xls" >
              </ExportSettings>

            <MasterTableView CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    
                        <telerik:GridBoundColumn DataField="Núm" HeaderText="Núm." UniqueName="Id_Prd"
                                    ReadOnly="true">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                         </telerik:GridBoundColumn>

                         <telerik:GridBoundColumn DataField="Ordenado" HeaderText="Ordenado." UniqueName="Id_Prd"
                                    ReadOnly="true">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                         </telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>

        </telerik:RadGrid>

   <p></p>

    </div>



</asp:Content>

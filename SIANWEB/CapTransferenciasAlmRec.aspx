<%@ Page Title="Recepción transferencias" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapTransferenciasAlmRec.aspx.cs" Inherits="SIANWEB.CapTransferenciasAlmRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .cssOcultar
        {
            display: none;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNaturaleza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntradaSalida">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divtotales" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            >
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png"  />
            
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 9pt;"  width="100%">
     
            <tr>
                <td>
                </td>
                <td>
                <table style="font-family: Verdana; font-size: 9pt;" width="100%" >
                   <tr>
                <td>
                &nbsp;
                </td>
                </tr>
                <tr>
                <td>
                </td>
                <td width="80px">
                <asp:Label  runat = "server" ID ="LblId_Tra"  Text = "Número" Font-Bold ="true" Width="50px">
                </asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtId_Tra">
                </asp:Label>
                </td>
                <td width="30px">
                </td>
                <td width="80px">
                   <asp:Label  runat = "server" ID ="LblTra_FechaEnvio"  Text = "Fecha envío" 
                        Font-Bold ="True"></asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="TxtTra_FechaEnvio" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                       <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblId_CdOrigenStr"  Text = "Remitente" Font-Bold ="True"></asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtId_CdOrigenStr">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="LblTra_FechaRecepcion"  Text = "Fecha recepción" 
                        Font-Bold ="True"></asp:Label>
                </td>
                <td>
                   <asp:Label  runat = "server" ID ="TxtTra_FechaRecepcion" >
                </asp:Label>
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="LblId_RemOrigen"  Text = "Remisión" 
                        Font-Bold ="True"></asp:Label>
                </td>
                <td>
                <asp:Label  runat = "server" ID ="TxtId_RemOrigen">
                </asp:Label>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                      <asp:Label  runat = "server" ID ="LblTra_Notas"  Text = "Notas" 
                        Font-Bold ="True"></asp:Label></td>
                <td  colspan="6">
                    <asp:Label  runat = "server" ID ="TxtTra_Notas"   ></asp:Label></td>
       <%--         <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>--%>
              <%--  <td width= "30%">
                </td>--%>
                </tr>
                          <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                 
                </td>
                <td>
                 
                </td>
                <td>
                </td>
                <td width= "30%">
                </td>
                </tr>
                <tr>
                <td>
                &nbsp;
                </td>
                </tr>
                
                </table>
          
    
                <table width="99%" runat="server" id="divtotales">
                    <tr>
                    <td  colspan="4">
                          
                                    <telerik:RadGrid ID="rgTransferenciaDet" runat="server" OnNeedDataSource="rgTransferencia_NeedDataSource"  AutoGenerateColumns="False" GridLines="None" 
                                        OnUpdateCommand= "rgTransferencia_UpdateCommand" >
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                        </ClientSettings>
                                        <MasterTableView EditMode="InPlace" NoMasterRecordsText="No se encontraron registros."
                                             DataKeyNames="UniqueID">
                                        
                                            <Columns>
                                             <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Id_Tra" HeaderText="Id_Tra" UniqueName="Id_Tra"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="UniqueID" HeaderText="UniqueID" UniqueName="UniqueID"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                        
                                           
                                                 <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_Prd"
                                                    DataType="System.Int32">
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblId_Prd" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                           ReadOnly="true" MinValue="1" Text='<%# Eval("Id_Prd") %>' BackColor="Transparent" 
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn HeaderText="Producto" DataField="Prd_Descripcion" UniqueName="Prd_Descripcion">
                                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrd_Descripcion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        
                                                        <telerik:RadTextBox ID="TxtPrd_Descripcion" runat="server" BackColor="Transparent"  ReadOnly="true" Width="100%"
                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrd_Presentacion" runat="server" Text='<%# Eval("Prd_Presentacion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="100%" ID="TxtPrd_Presentacion" BackColor="Transparent"  runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Presentacion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn DataField="TraD_Cant" HeaderText="Cant. Enviada" 
                                                    UniqueName="TraD_Cant">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_Cant" runat="server" MinValue="1" BackColor="Transparent" 
                                                            MaxLength="9" Width="50px" Text='<%# Eval("TraD_Cant") %>' ReadOnly = "true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_Cant" runat="server" Text='<%# Eval("TraD_Cant") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                   <telerik:GridTemplateColumn DataField="TraD_CantRec" HeaderText="Cant. Recibir" 
                                                    UniqueName="TraD_CantRec">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_CantRec" runat="server" MinValue="0" autopostback="true"
                                                            MaxLength="9" Width="50px" Text='<%# Eval("TraD_CantRec") %>'  OnTextChanged= "TxtTraD_CantRec_TextChanged" >
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_CantRec" runat="server" Text='<%# Eval("TraD_CantRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="TraD_Diferencia" HeaderText="Diferencia" 
                                                    UniqueName="TraD_Diferencia">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_Diferencia" runat="server" MinValue="0" ReadOnly= "true" BackColor="Transparent" 
                                                            MaxLength="9" Width="50px" Text='<%# Eval("TraD_Diferencia") %>'  >
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_Diferencia" runat="server" Text='<%# Eval("TraD_Diferencia") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>


                                                  <telerik:GridTemplateColumn DataField="TraD_Costo" HeaderText="Costo" 
                                                    UniqueName="TraD_Costo">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_Costo" runat="server" MinValue="0" BackColor="Transparent" 
                                                            MaxLength="9" Width="50px" Text='<%# Eval("TraD_Costo") %>' ReadOnly = "true">
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_Costo" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("TraD_Costo")).ToString("N") %>' /></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                     
                                                  <telerik:GridTemplateColumn DataField="TraD_TotalEnv" HeaderText="Total enviado" 
                                                    UniqueName="TraD_TotalEnv">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_TotalEnv" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("TraD_TotalEnv") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                           
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_TotalEnv" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("TraD_TotalEnv")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="TraD_TotalRec" HeaderText="Total recibido" 
                                                    UniqueName="TraD_TotalRec">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="TxtTraD_TotalRec" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("TraD_TotalRec") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                           
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTraD_TotalRec" runat="server" 
                                                            Text='<%# Convert.ToDouble(Eval("TraD_TotalRec")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                     
                                             <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                    EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" HeaderText="" UpdateText="Actualizar">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </telerik:GridEditCommandColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                    </td>
                    <td>
                    </td>
                    </tr>
                        <tr ID="trTotalEnviado" runat="server" style="font-family: verdana; font-size: 10pt">
                        <td  width="50%">
                        </td>
                        <td align="right" width= "20%">
                                <asp:Label ID="LblTra_TotalEnv" runat="server" Text="Total enviado" Font-Bold = "True" 
                                   ></asp:Label>
                        </td>
                            <td align="right" width= "10%">
                           <asp:Label ID="TxtTra_TotalEnv" runat="server"  ></asp:Label>
                            </td>
                            <td width="20%" align="right">
                                </td>
                        </tr>
                        <tr ID="trTotalRecibido" runat="server" style="font-family: verdana; font-size: 10pt">
                        <td width="50%">
                        </td>
                        <td align="right"  width= "20%">
                                <asp:Label ID="LblTra_TotalRec" runat="server" Text="Total recibido" Font-Bold = "true"></asp:Label>
                        </td>
                            <td align="right"  width= "10%">
                            <asp:Label ID="TxtTra_TotalRec" runat="server" ></asp:Label>
                            </td>
                            <td align="right" width="20%">
                                </td>
                        </tr>
                 
                        <tr>
                            <td align="right">
                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                                <asp:HiddenField ID="HFId_CdOrigen" runat="server" />
                                <asp:HiddenField ID="HFCD_IVA" runat="server" />
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function initDialog() {

            }




            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                    case 'delete':
                        continuarAccion = Confirma();
                        break;
                    case 'save':
                        button.set_enabled(false);
                        continuarAccion = _ValidarFechaEnPeriodo();
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
                else {
                    if (window.frameElement != null) {
                        if (window.frameElement.radWindow)
                            oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                    }
                    else
                        window.open("login.aspx");
                }
                return oWindow;
            }

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }



            function CloseWindow() {
                var cerrarWindow = radalert('El campo de referencia se encuentra vacío', 330, 150, '');
                cerrarWindow.add_close(
                function () {
                    //debugger;                        
                });
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }



            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }















            var mostrando_mensaje = false;
            function AlertaFocus_Mostrado(mensaje, control) {
                if (mostrando_mensaje) {
                    return;
                }
                var oWnd = radalert(mensaje, 340, 150);
                mostrando_mensaje = true;

                oWnd.add_close(function () {
                    mostrando_mensaje = false;
                    var target = $find(control);
                    if (target != null && (target.enabled || target._enabled)) {
                        target.focus();
                    }
                });
            }
           

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

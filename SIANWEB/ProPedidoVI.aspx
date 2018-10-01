<%@ Page Title="Captación de pedidos" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI.aspx.cs" Inherits="SIANWEB.ProVentInst_PedidoCap" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls> 
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtIdCte">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTerritorioNom">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rg1_Resto" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubtotal" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtIva" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1_Resto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rg1_Resto" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                  <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubtotal" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtIva" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
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
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talles" AccessKey="E">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px">
                        <%--Width="885px" Height="480px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <%--<asp:Panel ID="General" runat="server">--%>
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="480px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="700px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="Y">
                                    <table>
                                    <tr>
                                    <td>
                                     <asp:Label ID="Label3" runat="server" Text="Folio"></asp:Label>
                                    </td>
                                    <td>
                                          <telerik:RadNumericTextBox ID="txtFolio" runat="server" MaxLength="9" MinValue="1"
                                                                Width="70px" ReadOnly="True" Enabled="False">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    <asp:Label ID="Label4" runat="server" Text="Acuerdo"></asp:Label>
                                    </td>
                                    <td>
                                     <telerik:RadNumericTextBox ID="txtClave" runat="server" MaxLength="9" MinValue="0"
                                                                Width="70px" ReadOnly="True" Enabled="False">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    <asp:Label ID="LblVersion" runat="server" Text="Versión"></asp:Label>
                                    </td>
                                    <td>
                                       <telerik:RadNumericTextBox ID="TxtVersion" runat="server" MaxLength="9" MinValue="0"
                                                                Width="70px" ReadOnly="True" Enabled="False">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                    </td>
                                    <td></td>
                                    <td  style="display:none">
                                    <asp:Label ID="LblPed_PedAcys" runat="server" Text="Pedido"></asp:Label>
                                    </td>
                                    <td  style="display:none">
                                     <telerik:RadTextBox ID="TxtPed_PedAcys" runat="server"  Width="100px"
                                                                MaxLength="50">
                                                            </telerik:RadTextBox>
                                    </td>
                                    <td></td>
                                    <td>
                                    <asp:Label ID="LblPed_ReqAcys" runat="server" Text="Orden compra / Requisición"></asp:Label>
                                    </td>
                                    <td>
                                           <telerik:RadTextBox ID="TxtPed_ReqAcys" runat="server"  Width="100px"
                                                                MaxLength="50">
                                                            </telerik:RadTextBox>
                                    </td>
                                    <td></td>
                                    <td style="display:none">
                                    <asp:Label ID="LblPed_OCAcys" runat="server" Text="Orden compra"></asp:Label>
                                    </td>
                                    <td  style="display:none">
                                     <telerik:RadTextBox ID="TxtPed_OCAcys" runat="server"  Width="100px"
                                                                MaxLength="50">
                                                            </telerik:RadTextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="LblCaptadoPor" runat="server" Text="Captado Por:"></asp:Label></td>
                                        <td colspan="2"><telerik:RadTextBox ID="txtPedCaptadorPor" runat="server"  ReadOnly="True" Enabled="False"  Width="200px" MaxLength="50"></telerik:RadTextBox></td>
                                    </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td valign="top">
                                         
                                                <table width = "550px">
                                                    <tr>
                                                        <td colspan="9">
                                                            <b>
                                                                <asp:Label ID="Label5" runat="server" Text="Contacto"></asp:Label></b>
                                                        </td>
                                                        
                                                    </tr>
                                                       <tr>
                                                        <td colspan="9">
                                                           &nbsp;
                                                        </td>
                                                        
                                                    </tr>
                                                      <tr>
                                                        <td colspan="8">
                                                           
                                                                <asp:Label ID="LblNombreCont" runat="server" Text="Nombre de la persona encargada de enviar el pedido"></asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td colspan= "8">
                                                            <telerik:RadTextBox ID="txtContactoNom" runat="server" onpaste="return false" Width="450px"
                                                                MaxLength="30" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Puesto"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtContactoPuesto" runat="server" onpaste="return false"
                                                                Width="175px" MaxLength="50" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                         <asp:Label ID="LblEmail" runat="server" Text="E-mail"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                         <telerik:RadTextBox ID="txtContactoMail" runat="server" onpaste="return false" Width="175px"
                                                                MaxLength="50" Enabled="False">
                                                                <ClientEvents OnKeyPress="Email" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td></td>
                                                         <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text="Teléfono"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtContactoTel" runat="server" onpaste="return false" Width="175px"
                                                                MaxLength="20" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloNumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td colspan="7"></td>
                                                    </tr>
                                               <tr>
                                                        <td colspan="9">
                                                           &nbsp;
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                  
                                                         <td colspan="3">
                                                         <b>
                                                                <asp:Label ID="Label6" runat="server" Text="Dirección de entrega producto"></asp:Label></b>
                                                         </td>
                                                        <td colspan="8">
                                                            <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarDireccionEntrega_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdCte"
                                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                        <tr>
                                                        <td width="60">
                                                            <asp:Label ID="Label9" runat="server" Text="Calle"></asp:Label>
                                                        </td>
                                                        <td width="200">
                                                            <telerik:RadTextBox ID="txtCalle" runat="server" MaxLength="40" 
                                                                onpaste="return false" Width="200px" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="No."></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtNo" runat="server" Width="65px" MaxLength="15" 
                                                                MinValue="0" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td style="width: 0">
                                                            <asp:Label ID="Label27" runat="server" Text="C.P."></asp:Label>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <telerik:RadNumericTextBox ID="txtCp" runat="server"
                                                                MaxLength="5" MinValue="0" Width="65px" Enabled="False">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator=""  />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Colonia"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox onpaste="return false" ID="txtColonia" runat="server" Width="200px"
                                                                MaxLength="40" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" Text="Municipio"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <telerik:RadTextBox onpaste="return false" ID="txtMunicipio" runat="server" Width="200px"
                                                                MaxLength="40" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" Text="Estado"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtEstado" runat="server" onpaste="return false" Width="200px"
                                                                MaxLength="20" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td colspan="7">
                                                            
                                                        </td>
                                                        
                                                    </tr>
                                                      
                                                     <tr>
                                                        <td colspan="9">
                                                           &nbsp;
                                                            <asp:HiddenField ID="HF_ID" runat="server" />
                                                             <asp:HiddenField ID="HiddenRebind" runat="server" />
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table>
                                                 
                                                   
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Cliente"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtIdCte" runat="server" MaxLength="9" MinValue="1"
                                                                ReadOnly="True" Width="70px" OnTextChanged="txtIdCte_TextChanged">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtClienteNom" runat="server" onpaste="return false" ReadOnly="True"
                                                                Width="300px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="False" />
                                                            <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtIdCte"
                                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Territorio"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtIdTer" runat="server" MaxLength="9" MinValue="1"
                                                                ReadOnly="True" Width="70px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="txtTerritorioNom" runat="server" EmptyMessage="Seleccionar cliente"
                                                                OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged" Width="300px"
                                                                Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                OnClientBlur="Combo_ClientBlur" OnSelectedIndexChanged="txtTerritorioNom_SelectedIndexChanged">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 25px; text-align: right; vertical-align: top">
                                                                                <asp:Label ID="LabelID" runat="server" Width="25px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                            </td>
                                                                            <td style="text-align: left">
                                                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                            <td>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text="Representante de ventas"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtIdRik" runat="server" MaxLength="9" MinValue="1"
                                                                ReadOnly="True" Width="70px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtRikNom" runat="server" onpaste="return false" ReadOnly="True"
                                                                Width="300px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Semana actual"></asp:Label>
                                                        </td>
                                                        <td>
                                                          <telerik:RadNumericTextBox ID="txtSemana" runat="server" MaxLength="9" MinValue="1"
                                                                ReadOnly="True" Width="70px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td >
                                                          
                                                            <telerik:RadNumericTextBox ID="txtFecha" runat="server"
                                                                MaxLength="9" MinValue="1" ReadOnly="True" Width="70px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Fecha de facturación"></asp:Label>
                                                    </td>
                                                    <td colspan= "2">
                                               
                                                           <telerik:RadDatePicker ID="rdFechaF" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="calTxtFecha1" runat="server">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput ID="DateInput1" runat="server" MaxLength="10">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                       


                                                        </td>
                                                    </tr>
                                                          <tr>
                                                    <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Fecha compromiso entrega"></asp:Label>
                                                    </td>
                                                    <td colspan= "2">
                                                     <telerik:RadDatePicker ID="rdFechaE" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="calTxtFecha2" runat="server">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput ID="DateInput2" runat="server" MaxLength="10">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>

                                                   
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width= "100%">
                                                    <tr>
                                                        <td>
                                                        <b>
                                                                <asp:Label ID="Label1" runat="server" Text="Documentación requerida para entrega"></asp:Label></b>
                                                      </td>
                                                    
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBox ID="ChkOrdCompra" runat="server" Text="Orden de compra / Release" 
                                                            Enabled="False" />
                                                    </td>
                                                    
                                                    </tr>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBox ID="ChckOrdReposicion" runat="server" Text="Orden de reposición" 
                                                            Enabled="False" />
                                                    </td>
                                                    
                                                    </tr>
                                                      <tr>
                                                    <td>
                                                        <asp:CheckBox ID="ChckFolio" runat="server" Text="Folio" Enabled="False" />
                                                    </td>
                                                    
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                    <asp:Label ID="LblEOtro" runat="server" Text="Otro"></asp:Label>
                                                    &nbsp;&nbsp; &nbsp;
                                                      <telerik:RadTextBox onpaste="return false" ID="TxtEOtro" runat="server" Width="450px"
                                                                MaxLength="225" Enabled="False">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                    </td>
                                                    </tr>

                                                  
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                  <table width="100%">                                                
                                                          <tr>
                                                <td>
                                                    <asp:Label ID="Label36" runat="server" Text="Horario de recepeción"></asp:Label>
                                                </td>
                                                <td>
                                                 <telerik:RadTimePicker ID="txtRHoraam1" runat="server" Culture="es-MX"  Enabled="False">
                                                        <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView1" runat="server"  Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de recepción">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                       LabelCssClass="" Width="">
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label74" runat="server" Text="a"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTimePicker ID="txtRHoraam2" runat="server" Culture="es-MX"  Enabled="False">
                                                        <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView2" runat="server"  Culture="es-MX" EndTime="11:59:59"
                                                            HeaderText="Horario de recepción">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                              
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                                <td>
                                                            <asp:Label ID="Label75" runat="server" Text="y"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTimePicker ID="txtRHorapm1" runat="server" Culture="es-MX"  Enabled="False">
                                                        <Calendar ID="Calendar3" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView3" runat="server"  Culture="es-MX" HeaderText="Horario de recepción"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label76" runat="server" Text="a"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTimePicker ID="txtRHorapm2" runat="server" Culture="es-MX"  Enabled="False">
                                                        <Calendar ID="Calendar4" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                                        <TimeView ID="TimeView4" runat="server"  Culture="es-MX" HeaderText="Horario de recepción"
                                                            StartTime="12:00:00">
                                                        </TimeView>
                                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" ToolTip="Abrir popup de horarios" />
                                                        <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                            LabelCssClass="" Width="">
                                                        </DateInput>
                                                    </telerik:RadTimePicker>
                                                    
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                                </table>
                                                &nbsp;</td>
                                        </tr>
                                          <tr>
                                            <td colspan="2" align ="center" style="border-style: solid; color: #000000;">
                                                   <b>
                                                                <asp:Label ID="Label16" runat="server" Text="MODALIDAD DE PEDIDO"></asp:Label></b>
                                                &nbsp;</td>
                                        </tr>
                                           <tr>
                                            <td colspan="2" >
                                                   <table width="100%">
                                                   <tr>
                                                    <td></td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModFrencuenciaEstablecida" runat="server" 
                                                        Text="Frecuencia Establecida"  GroupName="ModalidadPedido" Enabled="False"  />
                                                </td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModOrdenAbierta" runat="server" 
                                                        Text="Orden Abierta con Reposición / Release"  GroupName="ModalidadPedido" 
                                                        Enabled="False" />
                                                </td>
                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModConsignacion"  runat="server" Text="Consignación" 
                                                        GroupName="ModalidadPedido" Enabled="False" />
                                                </td>

                                                <td colspan="2">
                                                 <asp:RadioButton ID="rdModInternet"  runat="server" Text="Pedido de Internet" 
                                                        GroupName="ModalidadPedido" Enabled="False" />
                                                </td>
                                                   </tr>
                                               </table>

                                                   </td>
                                                   </tr>
                                                   <tr align= "center" width="50%">
                                                   <td align= "center" colspan="2">
                                                   <table>
                                                   <tr>
                                                   <td>
                                                   <telerik:RadGrid ID="rgAcuerdos" runat="server" AutoGenerateColumns="False"
                                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                        BorderStyle="Solid" CellSpacing="0"  Height="90px" Width="650" >
                                                        <MasterTableView >    
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="Id_Prd" 
                                                                     HeaderText="Num. Producto" 
                                                                    UniqueName="Id_Prd" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Prd_Descripcion"
                                                                    HeaderText="Producto" UniqueName="Producto">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridCheckBoxColumn DataField="Acs_Lunes" DataType="System.Boolean" 
                                                                    HeaderText="L" 
                                                                    UniqueName="Acs_Lunes"  HeaderStyle-Width="40px" ItemStyle-Width="40px" >
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </telerik:GridCheckBoxColumn>
                                                                  <telerik:GridCheckBoxColumn DataField="Acs_Martes" DataType="System.Boolean" 
                                                                    HeaderText="M" 
                                                                    UniqueName="Acs_Martes"  HeaderStyle-Width="40px" ItemStyle-Width="40px" >
                                                                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </telerik:GridCheckBoxColumn>
                                                                  <telerik:GridCheckBoxColumn DataField="Acs_Miercoles" DataType="System.Boolean" 
                                                                    HeaderText="M" 
                                                                    UniqueName="Acs_Miercoles" HeaderStyle-Width="40px" ItemStyle-Width="40px"  >
                                                                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                     </telerik:GridCheckBoxColumn>
                                                                   <telerik:GridCheckBoxColumn DataField="Acs_Jueves" DataType="System.Boolean" 
                                                                    HeaderText="J" 
                                                                    UniqueName="Acs_Jueves"  HeaderStyle-Width="40px" ItemStyle-Width="40px" >
                                                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </telerik:GridCheckBoxColumn>
                                                                       <telerik:GridCheckBoxColumn DataField="Acs_Viernes" DataType="System.Boolean" 
                                                                    HeaderText="V" 
                                                                    UniqueName="Acs_Viernes"  HeaderStyle-Width="40px" ItemStyle-Width="40px"  >
                                                                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </telerik:GridCheckBoxColumn>
                                                                       <telerik:GridCheckBoxColumn DataField="Acs_Sabado" DataType="System.Boolean" 
                                                                    HeaderText="S" 
                                                                    UniqueName="Acs_Sabado"  HeaderStyle-Width="40px" ItemStyle-Width="40px">
                                                                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </telerik:GridCheckBoxColumn>
                                                                     <telerik:GridBoundColumn DataField="Acs_Documento" 
                                                                   HeaderText="Doc. de entrega" 
                                                                    UniqueName="Acs_Documento"  HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                                                                         <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>

                                                            <EditFormSettings>
                                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                                            <PagerStyle  />

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </MasterTableView>
                                                       
                                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="200px">
                                                </Scrolling>
                                                 <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                                    
                                                        <PagerStyle/>
                                                        <FilterMenu EnableImageSprites="False">
                                                        </FilterMenu>
                                                    
                                                    </telerik:RadGrid>
                                                   </td>
                                                   </tr>
                                                   
                                                   </table>
                                                   </td>
                                                   </tr>
                                        </tr>
                                            <tr>
                                            <td colspan="2" align ="center" style="border-style: solid; color: #000000;">
                                                   <b>
                                                                <asp:Label ID="Label17" runat="server" Text="CONTACTOS DEL CLIENTE"></asp:Label></b>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">
                                        <table width="100%">
                                             <tr>
                                                <td> </td>
                                                <td> <asp:Label ID="Label18" runat="server" Text="Almacén" />
                                                </td> <td>
                                                    <telerik:RadTextBox ID="txtContactoClientealmacen" runat="server" Width="257px" 
                                                         MaxLength="50" Enabled="False">
                                                       
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                                <td> <asp:Label ID="Label28" runat="server" Text="Teléfono" /> </td>
                                                <td> <telerik:RadNumericTextBox ID="txtContactoClientealmacenTel" runat="server" 
                                                        Width="100px" MaxLength="9"
                                                        MinValue="1" Enabled="False">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                       
                                                    </telerik:RadNumericTextBox> </td>
                                                <td></td>
                                                <td> <asp:Label ID="Label29" runat="server" Text="E-mail" /> </td>
                                                <td> <telerik:RadTextBox ID="txtContactoClientealmacenEmail" runat="server" 
                                                        Width="200px" MaxLength="50" Enabled="False">
                                                       
                                                    </telerik:RadTextBox> </td>
                                                <td> </td>
                                            </tr>
                                        </table>
                                        <table  width="100%">
                                            <tr>
                                                <td>&#160;</td>
                                            </tr>
                                            <tr>
                                                <td>&#160;</td>
                                            </tr>
                                            <tr>
                                            <td colspan="2">
                                                <table  width="100%">
                                                    <tr>
                                                       <td>
                                                            <asp:Label ID="lblNota" runat="server" Text="Notas"></asp:Label>
                                                        </td>
                                                        <td rowspan="2" valign="top">
                                                            <telerik:RadTextBox ID="txtNotas" runat="server" Height="45px" MaxLength="500" TextMode="MultiLine"
                                                                Width="500px">
                                                                <ClientEvents  OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            </tr>
                                        </table>
                                        </td>
                                        </tr>                                
                                        
                                    </table>
                                    <%-- </asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="380px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="380px" OnClientResized="onResize"
                                    BorderStyle="None">

                                    <%--<asp:Panel ID="Panel1" runat="server" Width="880px" Height="380px" ScrollBars="Horizontal">--%>
                                    <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        OnItemCommand="rg1_ItemCommand" OnNeedDataSource="rg1_NeedDataSource" OnItemDataBound="rg1_ItemDataBound"
                                        PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        AllowPaging="false" HeaderStyle-HorizontalAlign="Center" Enabled="true"
                                        AllowMultiRowSelection="true">

                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
                                              </ClientSettings>

                                        <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                            EditMode="InPlace" DataKeyNames="Ped_Asignar" TableLayout="Fixed">

                                            <CommandItemTemplate>
                                                   <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Visible='<%# !rg1.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="Imagenes/iconos/document_edit.png"/>Agregar</asp:LinkButton>&nbsp;&nbsp;
                                                  <asp:LinkButton ID="LinkButton1" OnClientClick="javascript:return confirm('¿Desea eliminar los productos seleccionados?')"
                                                     runat="server" CommandName="DeleteSelected"><img style="border:0px;vertical-align:middle;" alt="" src="Imagenes/iconos/document_delete.png"/>Eliminar</asp:LinkButton>&nbsp;&nbsp;
                                            </CommandItemTemplate>

                                            <Columns>
                                                    <telerik:GridClientSelectColumn UniqueName="chkItem">
                                                        <HeaderStyle Width="30px" />
                                                    </telerik:GridClientSelectColumn>


                                                <telerik:GridBoundColumn DataField="Id_PrdOld" Display="false">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Prod">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProd" runat="server" Text='<%# Bind("Id_Prd") %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtProd" runat="server" MinValue="1" Text='<%# Bind("Id_Prd") %>'
                                                            OnLoad="txtProducto_Load" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                            Width="30px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Id_PrdOld").ToString() == "" ? true : false %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="OnIdPrdLoad" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("Prd_Descripcion") %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Text='<%# Bind("Prd_Descripcion") %>'
                                                            Enabled='<%# DataBinder.Eval(Container.DataItem, "Id_PrdOld").ToString() == "" ? true : false %>'
                                                            Width="160px">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="180px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Presen." UniqueName="Prd_Presentacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPresent" runat="server" Text='<%# Bind("Prd_Presentacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPresent2" runat="server" Text='<%# Bind("Prd_Presentacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Unidad" HeaderText="Unidad" UniqueName="Prd_Unidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelUnidad" runat="server" Text='<%# Bind("Prd_Unidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelUnidad2" runat="server" Text='<%# Bind("Prd_Unidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes1" HeaderText="Mes1" UniqueName="mes1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes1" runat="server" Text='<%# Bind("mes1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes12" runat="server" Text='<%# Bind("mes1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes2" HeaderText="Mes2" UniqueName="mes2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes2" runat="server" Text='<%# Bind("mes2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes22" runat="server" Text='<%# Bind("mes2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes3" HeaderText="Mes1" UniqueName="mes3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes3" runat="server" Text='<%# Bind("mes3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelmes32" runat="server" Text='<%# Bind("mes3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                    <HeaderStyle Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Prd_Cantidad" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_Cantidad" ) %>'
                                                            OnTextChanged="txtCantidad_TextChanged" Width="40px" AutoPostBack="true">
                                                            <ClientEvents OnLoad="Cantidad_Load" OnBlur="Cantidad_Blur" OnKeyPress="handleClickEvent" />
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Precio" HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                            OnTextChanged="txtPrecio_TextChanged" Width="50px" AutoPostBack="true">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                            <ClientEvents OnLoad="Precio_Load" OnBlur="Precio_Blur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_PrecioAcys" HeaderText="Precio vta." UniqueName="Prd_PrecioAcys"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecioAcys" runat="server" Text='<%# Bind("Prd_PrecioAcys","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPrecioAcys" runat="server" Text='<%# Bind("Prd_PrecioAcys","{0:N2}") %>'
                                                            Width="50px">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Importe" HeaderText="Importe" UniqueName="Prd_Importe">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("Prd_Importe","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" Text='<%# Bind("Prd_Importe","{0:N2}") %>'
                                                            Width="50px" ReadOnly="true">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                            <ClientEvents OnLoad="Importe_Load" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="cmbDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                            LoadingMessage="Cargando..." Width="60px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="Mod" HeaderText="Mod. Vta. Inst." Display="false">
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label23" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) ? "Si" : "No" %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkModTemp" runat="server" AutoPostBack="true" OnCheckedChanged="chkMod_CheckedChanged"
                                                            Checked='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>' />
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Dia" HeaderText="Día" UniqueName="Acs_Dia" Display="false">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDia" runat="server" Text='<%# Bind("Acs_DiaStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="cmbDia" runat="server" SelectedValue='<%# Bind("Acs_Dia") %>'
                                                            LoadingMessage="Cargando..." Width="60px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Lunes" Value="L" />
                                                                <telerik:RadComboBoxItem Text="Martes" Value="M" />
                                                                <telerik:RadComboBoxItem Text="Miércoles" Value="Mi" />
                                                                <telerik:RadComboBoxItem Text="Jueves" Value="J" />
                                                                <telerik:RadComboBoxItem Text="Viernes" Value="V" />
                                                                <telerik:RadComboBoxItem Text="Sábado" Value="S" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Frecuencia" HeaderText="Frec. cada (n) sem."
                                                    UniqueName="Acs_Frecuencia" Display="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%# Bind("Acs_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acs_Frecuencia") %>'
                                                            Width="55px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <HeaderStyle Width="75px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                    UpdateText="Actualizar" InsertText="Aceptar" UniqueName="EditCommandColumn">
                                                    <HeaderStyle Width="65px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                    ConfirmText="¿Borrar este detalle?" Text="Borrar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                    ConfirmDialogWidth="350px">
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridTemplateColumn DataField="Ped_Asignar" HeaderText="Ped_Asignar" UniqueName="Ped_Asignar"
                                                    Visible="false">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad2" runat="server" Text='<%# Bind("Ped_Asignar" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad2" runat="server" Text='<%# Bind("Ped_Asignar" ) %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                            <%--<Scrolling AllowScroll="True" SaveScrollPosition="True" ScrollHeight="260px" UseStaticHeaders="True" />--%>
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <%--</asp:Panel>--%>

                                    <br />
                                    <br /> 

                                    Resto de productos del Acys
                                    <br />

                                    <telerik:RadGrid ID="rg1_Resto" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        OnItemCommand="rg1_Resto_ItemCommand" OnNeedDataSource="rg1_Resto_NeedDataSource" OnItemDataBound="rg1_Resto_ItemDataBound"
                                        PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        AllowPaging="false" HeaderStyle-HorizontalAlign="Center" Enabled="true"
                                        AllowMultiRowSelection="true">

                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
                                              </ClientSettings>

                                        <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                            EditMode="InPlace" DataKeyNames="Ped_Asignar" TableLayout="Fixed">

                                            <CommandItemTemplate>
                                                   <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Agregar" Visible='<%# !rg1.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="Imagenes/iconos/document_edit.png"/>Agregar Productos</asp:LinkButton>&nbsp;&nbsp;
                                                    </CommandItemTemplate>

                                            <Columns>
                                                    <telerik:GridClientSelectColumn UniqueName="chkItem">
                                                        <HeaderStyle Width="30px" />
                                                    </telerik:GridClientSelectColumn>


                                                <telerik:GridBoundColumn DataField="Id_PrdOld" Display="false">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Prod">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProd_resto" runat="server" Text='<%# Bind("Id_Prd") %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtProd" runat="server" MinValue="1" Text='<%# Bind("Id_Prd") %>'
                                                            OnLoad="txtProducto_Load" AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged"
                                                            Width="30px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Id_PrdOld").ToString() == "" ? true : false %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="OnIdPrdLoad" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("Prd_Descripcion") %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Text='<%# Bind("Prd_Descripcion") %>'
                                                            Enabled='<%# DataBinder.Eval(Container.DataItem, "Id_PrdOld").ToString() == "" ? true : false %>'
                                                            Width="160px">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="180px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Presentacion" HeaderText="Presen." UniqueName="Prd_Presentacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Bind("Prd_Presentacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("Prd_Presentacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Unidad" HeaderText="Unidad" UniqueName="Prd_Unidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("Prd_Unidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("Prd_Unidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes1" HeaderText="Mes1" UniqueName="mes1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label35" runat="server" Text='<%# Bind("mes1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("mes1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes2" HeaderText="Mes2" UniqueName="mes2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("mes2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label39" runat="server" Text='<%# Bind("mes2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="mes3" HeaderText="Mes1" UniqueName="mes3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label40" runat="server" Text='<%# Bind("mes3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label41" runat="server" Text='<%# Bind("mes3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Cantidad" HeaderText="Cant." UniqueName="Acys_Cantidad">
                                                    <HeaderStyle Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label42" runat="server" Text='<%# Bind("Prd_Cantidad" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Prd_Cantidad" ) %>'
                                                            OnTextChanged="txtCantidad_TextChanged" Width="40px" AutoPostBack="true">
                                                            <ClientEvents OnLoad="Cantidad_Load" OnBlur="Cantidad_Blur" OnKeyPress="handleClickEvent" />
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Precio" HeaderText="Precio vta." UniqueName="Prd_Precio">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Text='<%# Bind("Prd_Precio","{0:N2}") %>'
                                                            OnTextChanged="txtPrecio_TextChanged" Width="50px" AutoPostBack="true">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                            <ClientEvents OnLoad="Precio_Load" OnBlur="Precio_Blur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_PrecioAcys" HeaderText="Precio vta." UniqueName="Prd_PrecioAcys"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label44" runat="server" Text='<%# Bind("Prd_PrecioAcys","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPrecioAcys" runat="server" Text='<%# Bind("Prd_PrecioAcys","{0:N2}") %>'
                                                            Width="50px">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Importe" HeaderText="Importe" UniqueName="Prd_Importe">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label45" runat="server" Text='<%# Bind("Prd_Importe","{0:N2}") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" Text='<%# Bind("Prd_Importe","{0:N2}") %>'
                                                            Width="50px" ReadOnly="true">
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                            <ClientEvents OnLoad="Importe_Load" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Doc" HeaderText="Doc. de entrega" UniqueName="Acs_Doc">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label46" runat="server" Text='<%# Bind("Acs_Doc") %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="cmbDocumento" runat="server" Text='<%# Bind("Acs_Doc") %>'
                                                            LoadingMessage="Cargando..." Width="60px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Factura" Value="F" />
                                                                <telerik:RadComboBoxItem Text="Remisión" Value="R" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="Mod" HeaderText="Mod. Vta. Inst." Display="false">
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label47" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) ? "Si" : "No" %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="chkMod_CheckedChanged"
                                                            Checked='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>' />
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Dia" HeaderText="Día" UniqueName="Acs_Dia" Display="false">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label48" runat="server" Text='<%# Bind("Acs_DiaStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="cmbDia" runat="server" SelectedValue='<%# Bind("Acs_Dia") %>'
                                                            LoadingMessage="Cargando..." Width="60px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Lunes" Value="L" />
                                                                <telerik:RadComboBoxItem Text="Martes" Value="M" />
                                                                <telerik:RadComboBoxItem Text="Miércoles" Value="Mi" />
                                                                <telerik:RadComboBoxItem Text="Jueves" Value="J" />
                                                                <telerik:RadComboBoxItem Text="Viernes" Value="V" />
                                                                <telerik:RadComboBoxItem Text="Sábado" Value="S" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Acs_Frecuencia" HeaderText="Frec. cada (n) sem."
                                                    UniqueName="Acs_Frecuencia" Display="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label49" runat="server" Text='<%# Bind("Acs_Frecuencia" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFrecuencia" runat="server" DbValue='<%# Bind("Acs_Frecuencia") %>'
                                                            Width="55px" Enabled='<%# DataBinder.Eval(Container.DataItem, "Mod") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Mod")) %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <EnabledStyle HorizontalAlign="Right" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <HeaderStyle Width="75px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn DataField="Ped_Asignar" HeaderText="Ped_Asignar" UniqueName="Ped_Asignar"
                                                    Visible="false">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label50" runat="server" Text='<%# Bind("Ped_Asignar" ) %>'></asp:Label></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad2" runat="server" Text='<%# Bind("Ped_Asignar" ) %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                            <%--<Scrolling AllowScroll="True" SaveScrollPosition="True" ScrollHeight="260px" UseStaticHeaders="True" />--%>
                                        </ClientSettings>
                                    </telerik:RadGrid>

                                </telerik:RadPane>
                            </telerik:RadSplitter>
                            <%--  <table>                              
                                <tr>
                                    <td align="right">--%>
                            <asp:Panel ID="divTotales" runat="server">
                                <table width="99%">
                                    <tr>
                                        <td align="right" width="350">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right" width="90">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label24" runat="server" Text="Subtotal "></asp:Label>
                                        </td>
                                        <td align="right">
                                            <telerik:RadNumericTextBox ID="txtSubtotal" runat="server" ReadOnly="True" Width="70px"
                                                MinValue="0">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label25" runat="server" Text="I.V.A."></asp:Label>
                                        </td>
                                        <td align="right">
                                            <telerik:RadNumericTextBox ID="txtIva" runat="server" ReadOnly="True" Width="70px"
                                                MinValue="0">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label26" runat="server" Text="Total"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" ReadOnly="True" Width="70px"
                                                MinValue="0">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--       </td>
                                </tr>
                            </table>          --%>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HF_InicioSemana" runat="server" />
                    <asp:HiddenField ID="HF_FinSemana" runat="server" />
                    <asp:HiddenField ID="HF_FechaActual" runat="server" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            function AbrirVentana_InvIns(fecha, orden, Acys) {
                var cte = $find('<%=txtIdCte.ClientID%>');
                var oWnd = radopen("ProPedidoVI_InvIns.aspx?fecha=" + fecha + "&orden=" + orden + "&Id_Acs=" + Acys + "&cte=" + cte.get_value(), "ProPedidoVI_InvIns");
                oWnd.center();
            }
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                switch (button.get_value()) {
                    case 'save':
                        //continuarAccion = _ValidarFechaEnPeriodo();
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function ClientTabSelecting(sender, args) {
            }

            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('ok');
                } else {
                    CloseWindow();
                }
            }

            function confirmCallBackFnGuardar(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (!arg) {
                    ajaxManager.ajaxRequest('continuar');
                }
            }

            function confirmCallBackFnPrecio(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('continuar');
                } else
                { ajaxManager.ajaxRequest('rebind'); }
            }

            /*IMPORTE*/
            var precio;
            var cantidad;
            var importe;
            function Precio_Load(sender, args) {
                precio = sender;
            }
            function Cantidad_Load(sender, args) {
                cantidad = sender;
            }
            function Importe_Load(sender, args) {
                importe = sender;
            }

            function Precio_Blur(sender, args) {
                importe.set_value(cantidad.get_value() * precio.get_value());
            }
            function Cantidad_Blur(sender, args) {
                importe.set_value(cantidad.get_value() * precio.get_value());
            }

            /*PRODUCTO*/
            var IdPrd;
            var txtId_Prd;
            var DescPrd;
            function OnIdPrdLoad(sender, args) {
                IdPrd = sender;
                txtId_Prd = sender;
            }
            function OnDescripcionPrdLoad(sender, args) {
                DescPrd = sender;
            }
            function IdPrd_OnBlur(sender, eventArgs) {
                OnBlur(sender, DescPrd);
            }
            function DescPrd_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), IdPrd);
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= rdFechaF.ClientID %>');
                return txtFecha._dateInput;
            }
            /*CERRAR VENTANA*/
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
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtIdTer.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= txtTerritorioNom.ClientID %>'));
            }
            //--------------------------------------------------------------------------------------------------
            // Abre ventana para el reporte de imprsion de Orden de Compra
            //--------------------------------------------------------------------------------------------------
            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
                CloseWindow();
            }

            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }

            function abrirBuscar() {
                var cte = $find('<%=txtIdCte.ClientID%>');
                var oWnd = radopen("Ventana_Buscar.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                oWnd.center();
            }
            function AbrirBuscarDireccionEntrega() {
                var cte = $find('<%=txtIdCte.ClientID%>');
                var oWnd = radopen("Ventana_Buscar.aspx?DirEnt=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                oWnd.center();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

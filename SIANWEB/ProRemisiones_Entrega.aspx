<%@ Page Title="Remisiones en entrega" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="ProRemisiones_Entrega.aspx.cs" Inherits="SIANWEB.ProRemisiones_Entrega" %>
 

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%"  />                      
                </UpdatedControls>
            </telerik:AjaxSetting>                
            <telerik:AjaxSetting AjaxControlID="rgRemisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>            
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>            
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>            
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        
         <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right; width:150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>   
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:HiddenField ID="HF_ID" runat="server" />
                    <asp:HiddenField ID="HF_PED" runat="server" />
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td width="70">
                                &nbsp;</td>
                            <td width="10">
                                &nbsp;</td>
                            <td width="70">
                                &nbsp;</td>
                            <td width="90">
                                &nbsp;</td>
                            <td width="50">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                              <asp:Label ID="LblNombre" runat="server" Text="Nombre de cliente"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" Runat="server" Width="300px" MaxLength="140">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente" Runat="server" Width="70px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator=""/>  
                                    <ClientEvents OnKeyPress="SoloNumerico" />                                  
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                           <td>
                                <asp:Label ID="LblCliente2" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" Runat="server" Width="70px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator=""/>  
                                    <ClientEvents OnKeyPress="SoloNumerico" />                                  
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblFecha1" runat="server" Text="Fecha inicial"></asp:Label>                                 
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" Runat="server" Width="155px">                                
                                 <Calendar ID="Calendar1" runat="server">                                       
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar" TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="width:65px">
                                <asp:Label ID="LblFecha2" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" Runat="server" Width="155px">
                                    <Calendar ID="Calendar2" runat="server">                                       
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar" TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                             <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
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
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                <asp:Panel ID="Panel1" runat="server" Width="950px">
                    <telerik:RadGrid ID="rgRemisiones" runat="server" AutoGenerateColumns="False" Width="900px" 
                        GridLines="None" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="rgRemisiones_ItemDataBound"                         
                         OnItemCommand="rgRemisiones_ItemCommand" OnPageIndexChanged="rgRemisiones_PageIndexChanged"                     
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                            <MasterTableView ClientDataKeyNames="Id_Rem">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                                <Columns>                                            
                                    <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                         <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Numero" HeaderText="Número" UniqueName="Numero"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}">              
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>   
                                        <ItemStyle HorizontalAlign="Center" />                     
                                    </telerik:GridBoundColumn>   <telerik:GridBoundColumn DataField="Fecha2" HeaderText="Fecha2" UniqueName="Fecha2" DataFormatString="{0:dd/MM/yyyy}" Visible="false">              
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>   
                                        <ItemStyle HorizontalAlign="Center" />                     
                                    </telerik:GridBoundColumn>               
                                    <telerik:GridBoundColumn DataField="Pedido" HeaderText="Pedido" UniqueName="Pedido"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Num_Cliente" HeaderText="Núm. cte." UniqueName="Num_Cliente"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente"> 
                                        <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>  
                                        <ItemStyle HorizontalAlign="Left" />           
                                    </telerik:GridBoundColumn>    
                                    <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow" 
                                        ConfirmText="¿Desea confirmar la entrega de la remisi&oacute;n <b>#[[ID]]</b> ? <br />" Text="Autorizar entrega" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                        UniqueName="Autorizar" Visible="true" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass ="aceptar">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridButtonColumn>                                     
                                </Columns>
                            </MasterTableView>
                     <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />                        
                    </telerik:RadGrid>
             </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFecha1.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFecha2.ClientID %>'));
            }           
     </script>
    </telerik:RadCodeBlock>
</asp:Content>

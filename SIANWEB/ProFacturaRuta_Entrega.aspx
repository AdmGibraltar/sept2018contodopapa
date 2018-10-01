<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="ProFacturaRuta_Entrega.aspx.cs" Inherits="SIANWEB.ProFacturaRuta_Entrega" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%"  />                      
                </UpdatedControls>
            </telerik:AjaxSetting>                
            <telerik:AjaxSetting AjaxControlID="rgFactura">
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
                            <td width="50">
                                &nbsp;</td>
                            <td width="90">
                                &nbsp;</td>
                            <td width="50">
                                &nbsp;</td>
                        </tr> 
                        <tr>
                            <td>
                                <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial"></asp:Label>                                 
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechaini" Runat="server" Width="155px">                                
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
                                <asp:Label ID="LblFechafin" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechafin" Runat="server" Width="155px">
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblEmbarque" runat="server" Text="Embarque"></asp:Label>
                                </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEmbarque" Runat="server" Width="125px" MinValue="0" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator=""/>  
                                    <ClientEvents OnKeyPress="SoloNumerico" />                                  
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                             <td>
                                <asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label>
                            </td>                            
                            <td >
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="130px">                                    
                                </telerik:RadComboBox>
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
                            <td style="width:70px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                <asp:Panel ID="Panel1" runat="server" Width="950px" >
                    <telerik:RadGrid ID="rgFactura" runat="server" AutoGenerateColumns="False" Width="900px" 
                        GridLines="None" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="rgFactura_ItemDataBound"                         
                         OnItemCommand="rgFactura_ItemCommand" OnPageIndexChanged="rgFactura_PageIndexChanged"                     
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                            <MasterTableView ClientDataKeyNames="Id_Emb">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                                <Columns>         
                                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                         <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id_Cte" UniqueName="Id_Cte" Visible="false">                                        
                                    </telerik:GridBoundColumn>     
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial1" HeaderText="Usuario" UniqueName="Cte_NomComercial1"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Emb" HeaderText="Embarque" UniqueName="Id_Emb"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                       
                                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}">              
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>   
                                        <ItemStyle HorizontalAlign="Center" /> 
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Dia" HeaderText="Día" UniqueName="Dia"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Chofer" HeaderText="Chofer" UniqueName="Chofer"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Camion" HeaderText="Camioneta" UniqueName="Camion"> 
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>   
                                         <ItemStyle HorizontalAlign="Left" />          
                                    </telerik:GridBoundColumn>    
                                    <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow" 
                                        ConfirmText="¿Desea confirmar la entrega del embarque de factura <b>#[[ID]]</b> ? <br />" Text="Autorizar entrega" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
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
                LimpiarTextBox($find('<%= txtEmbarque.ClientID %>'));
                LimpiarTextBox($find('<%= cmbEstatus.ClientID %>'));              
                LimpiarDatePicker($find('<%= dpFechaini.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFechafin.ClientID %>'));
            }           
     </script>
    </telerik:RadCodeBlock>
</asp:Content>
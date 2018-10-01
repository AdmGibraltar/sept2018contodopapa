<%@ Page Title="Administrador para captación de pedidos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI_Admin.aspx.cs" Inherits="SIANWEB.ProAdminCapPedido_VentInst" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
<link href="../Styles/MenuTab.css" rel="stylesheet" type="text/css" />
<style>
    .PanelId
    {
      display:none;   
    }
</style>

<asp:TextBox runat="server" ID="PanelId" CssClass="PanelId" Text="7"></asp:TextBox>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgInternet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />                           
                <telerik:RadToolBarButton CommandName="edit" Value="edit" CssClass="modificar" ToolTip="Editar"
                    ImageUrl="~/Imagenes/blank.png" />
               <telerik:RadToolBarButton CommandName="question" Value="question" ToolTip="Protocolos de acción" CssClass="question"
                    ImageUrl="~/Imagenes/blank.png"/>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                    <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Enviar a excel"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Pedido Nuevo y/o Esporádico " CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
                     <telerik:RadToolBarButton CommandName="rechazar" Value="rechazar" CssClass="delete" ToolTip="Rechazar seleccionados"
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
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
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
                                <asp:Label ID="Label3" runat="server" Text="Cliente inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCteIni" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Cliente final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCteFin" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="CmbEstatus" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="--Todos--" 
                                            Value="T" />
                                        <telerik:RadComboBoxItem runat="server" Text="Pendiente" Selected="True" Value="P" />
                                        <telerik:RadComboBoxItem runat="server" Text="Captado"   Value="C" />
                                        <telerik:RadComboBoxItem runat="server" Text="Rechazado" Value="X" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                             
                           
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Territorio inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerIni" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Territorio final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerFin" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Vigencia" runat="server" Text="Vigencia"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="CmbVigencia" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="--Todos--" 
                                            Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Vencido" Value="0" />
                                        <telerik:RadComboBoxItem runat="server" Text="Vigente" Value="1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            
                        
                           
                        </tr>
                          <tr>
                            <td>
                                <asp:Label ID="LblSemIni" runat="server" Text="Semana inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtSemIni" runat="server" Width="70px" MaxLength="2"
                                    MinValue="1" MaxValue="53">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblSemFin" runat="server" Text="Semana final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtSemFin" runat="server" Width="70px" MaxLength="2"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                           <td>
                                <asp:Label ID="LblCredito" runat="server" Text="Crédito"></asp:Label>
                            </td>
                            <td>
                                <telerik:radcombobox ID="CmbCredito" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="--Todos--" Value=""></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="SI" Value="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="NO" Value="False"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:radcombobox>
                            </td>
                            
                        
                           
                        </tr>
                           <tr>
                            <td>
                                <asp:Label ID="LblAnioIni" runat="server" Text="Año inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtAnioIni" runat="server" Width="70px" MaxLength="4"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblAnioFin" runat="server" Text="Año final"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtAnioFin" runat="server" Width="70px" MaxLength="4"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Garantía</td>
                            <td colspan="3">
                                <telerik:RadComboBox runat="server" ID="rcbTipoGarantia">
                                </telerik:RadComboBox>
                                </td>
                            <td>
                         &nbsp;
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Nombre del cliente"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="260px" MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td width="10">
                            </td>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                &nbsp;</td>
                            <td>
                          <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                        ToolTip="Buscar"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="80">
                            </td>
                            <td width="50">
                                &nbsp;  
                            </td>
                        </tr>
                    </table>


 <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Pedidos VI" AccessKey="G"
            PageViewID="RadPageViewVI" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Pedidos Internet" PageViewID="RadPageViewInternet" AccessKey="E">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>


<telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
BorderWidth="1px">
<%--Width="885px" Height="480px">--%>
    <telerik:RadPageView ID="RadPageViewVI" runat="server">
    
    
<div id="Menu1" style="height:600px; overflow:scroll" >
    <table>
           <tr>
                 <td>
                       <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None" 
                                    OnNeedDataSource="RadGrid1_NeedDataSource"  AllowPaging="True" AllowdOnPageIndexChanged="rg1_PageIndexChanged"
                                    OnItemCommand="rg1_ItemCommand" PageSize="100" OnItemDataBound="rg1_ItemDataBound" AllowMultiRowSelection="true">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros." DataKeyNames="Id_Acs,Id_Cte,Id_Ter,Id_TG">
                                        <Columns>
                                               <telerik:GridTemplateColumn  UniqueName="Incluir"  >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIncluir" runat="server" AutoPostBack="True"  OnCheckedChanged = "Seleccionar" Checked='<%# Eval("Seleccionado") %>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkIncluirTodos" runat="server" 
                                                                OnCheckedChanged="SeleccionarTodos" AutoPostBack="True"  />
                                                            </HeaderTemplate>
                                                        </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Cte_Credito" HeaderText="crédito" UniqueName="Cte_Credito"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Acs" HeaderText="Id_Acs" UniqueName="Id_Acs"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Acs_Estatus" UniqueName="Acs_Estatus"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Vigencia" HeaderText="Acs_Vigencia" UniqueName="Acs_Vigencia"
                                                Display="false">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cte_Nom" HeaderText="Cliente" UniqueName="Cte_Nom">
                                                <HeaderStyle Width="400px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Id_Ter">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Cantidad" HeaderText="Venta instalada" UniqueName="Acs_Cantidad"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Semana" HeaderText="Semana de entrega" UniqueName="Acs_Semana">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="120px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Acs_Anio" HeaderText="Año" UniqueName="Acs_Anio">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="Cte_CreditoLetra" HeaderText="Crédito" UniqueName="Cte_CreditoLetra"
                                                Display="True">
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="Acs_VigenciaStr" HeaderText="Vigencia" UniqueName="Acs_VigenciaStr">
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn DataField="Acs_EstatusStr" HeaderText="Estatus" UniqueName="Acs_EstatusStr">
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Id_Ped" UniqueName="Id_Ped"
                                                Display="false">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Modalidad de Garantía" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" >
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblModalidadGarantia" Text='<%# Eval("ModalidadGarantia") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Captar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Captar" CommandName="captar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Rechazar" Text="Rechazar"
                                                  ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                                ConfirmText="¿Esta seguro que desea rechazar el pedido?"
                                                UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                ButtonCssClass="baja" Display="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                                ConfirmText="Se imprimirá el pago, tenga listo el formato en la impresora</br></br>"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir"
                                                Display="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                          <ClientSettings>   
                                             <Selecting AllowRowSelect="true" />                             
                                              
                                         </ClientSettings>
                                         
                         <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
           
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label8" runat="server" Text="Total a captar"></asp:Label>
                                <asp:Label ID="txtTotal" runat="server" Width="120px" Font-Bold="True">
                                   
                                </asp:Label>
                            </td>
                        </tr>
                    </table>

</div>
 </telerik:RadPageView>



    <telerik:RadPageView ID="RadPageViewInternet" runat="server">
    
<div id="Menu2" style="height:600px; overflow:scroll">
 <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgInternet" runat="server" AutoGenerateColumns="False" GridLines="None" 
                                    OnNeedDataSource="rgInternet_NeedDataSource"  AllowPaging="True" AllowdOnPageIndexChanged="rgInternet_PageIndexChanged"
                                    OnItemCommand="rgInternet_ItemCommand" PageSize="100" OnItemDataBound="rgInternet_ItemDataBound" OnPreRender="rgInternet_PreRender" AllowMultiRowSelection="true">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>
                                             <telerik:GridBoundColumn DataField="UnidadNegocio_Id" HeaderText="UnidadNegocio_Id" UniqueName="UnidadNegocio_Id"
                                                Display="false">
                                                </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus_Id" HeaderText="Estatus_Id" UniqueName="Estatus_Id"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Cte_Credito" HeaderText="crédito" UniqueName="Cte_Credito"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn  UniqueName="Incluir"  >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True"  OnCheckedChanged = "Seleccionar" Checked='<%# Eval("Seleccionado") %>' />
                                                            </ItemTemplate>
                                                   
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" 
                                                                OnCheckedChanged="SeleccionarTodos" AutoPostBack="True"  />
                                                            </HeaderTemplate>
                                                </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Id_Ter" UniqueName="Id_Ter"
                                                Display="false">
                                            </telerik:GridBoundColumn>

                                           <telerik:GridBoundColumn DataField="Num_Pedido" HeaderText="Núm. pedido" UniqueName="Num_Pedido">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>


                                             <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Nombre Cliente" UniqueName="Cte_NomComercial">
                                                <HeaderStyle Width="200px" />
                                               </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="UnidadNegocio_Nombre" HeaderText="Nombre Unidad de Negocio" UniqueName="UnidadNegocio_Nombre">
                                                <HeaderStyle Width="200px" />
                                               </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="Observaciones">
                                                <HeaderStyle Width="250px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre_Usuario" HeaderText="Usuario" UniqueName="Nombre_Usuario">
                                                <HeaderStyle Width="200px" />
                                            </telerik:GridBoundColumn>

                                             <telerik:GridBoundColumn DataField="Fecha_Requisicion" HeaderText="Fecha de Requisicion" UniqueName="Fecha_Requisicion"
                                              DataFormatString="{0:dd/MM/yy}">
                                                <HeaderStyle Width="200px" />
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn DataField="Estatus_Nombre" HeaderText="Estatus" UniqueName="Estatus_Nombre"
                                                Display="True">
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="Cte_CreditoLetra" HeaderText="Crédito" UniqueName="Cte_CreditoLetra"
                                                Display="True">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="Total">
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>



                                            <telerik:GridTemplateColumn HeaderText="Captar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" UniqueName="Captar" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="CaptarImgInternet" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Captar" CommandName="captar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Rechazar" Text="Rechazar"
                                                  ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                                ConfirmText="¿Esta seguro que desea rechazar el pedido?"
                                                UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                ButtonCssClass="baja" Display="True" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                                ConfirmText="Se imprimirá el pago, tenga listo el formato en la impresora</br></br>"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir"
                                                Display="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                          <ClientSettings>   
                                             <Selecting AllowRowSelect="true" />                             
                                              
                                         </ClientSettings>
                                         
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
           
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label10" runat="server" Width="120px" Font-Bold="True">
                                   
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
 
</div>

    </telerik:RadPageView>
</telerik:RadMultiPage>

                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
        <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
          <asp:HiddenField ID="HD_Semana" runat="server" Value="0" />
          <asp:HiddenField ID="HD_Anio" runat="server" Value="0" />
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <%--Pedido Captado--%>
            <telerik:RadWindow ID="AbrirVentana_PedidoCaptado" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="350px" Height="200px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Pedido a editar" Modal="True"
                OnClientClose="CerrarWindow_ClientEvent_Pedido" OnClientPageLoad="LimpiarBanderaRebind_Pedido">
            </telerik:RadWindow>
            <%-- PEDIDOS VENTA INSTALADA --%>
            <telerik:RadWindow ID="AbrirVentana_PedidoVI" runat="server" Behaviors="Move,Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="910px" Height="800px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="" Modal="True" OnClientClose="CerrarWindow_Event"
                ShowContentDuringLoad="False">
            </telerik:RadWindow>
            <%--REPORTES--%>
            <telerik:RadWindow ID="RWReporte" runat="server" Behaviors="Move,Close,Resize,Maximize"
                Opacity="100" VisibleStatusbar="False" Width="920px" Height="600px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Reporte" Modal="true" ShowContentDuringLoad="False"
                OnClientClose="CerrarWindow_Event">
            </telerik:RadWindow>
               <telerik:RadWindow ID="AbrirVentana_Protocolos" runat="server" Behaviors="Close"
                Opacity="100" VisibleStatusbar="False" Width="1120px" Height="600px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Protocolos de acción"
                Modal="True"  ShowContentDuringLoad="False">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // ---------------------
            // Variables de permiso
            // ---------------------
            var permisoGuardar = '<%= _PermisoGuardar %>'
            var permisoModificar = '<%= _PermisoModificar %>'
            var permisoEliminar = '<%= _PermisoEliminar %>'
            var permisoImprimir = '<%= _PermisoImprimir %>'

            function AbrirVentana_ProPedidoVI(Id, guardar, modificar, eliminar, imprimir, Anio, semana, Id_TG) {
                //debugger;
                var idTgQueryComponent = '';
                if (typeof (Id_TG) != 'undefined' && typeof (Id_TG) != undefined) {
                    idTgQueryComponent = "&Id_TG=" + Id_TG;
                }
                var oWnd = radopen("ProPedidoVI.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana + idTgQueryComponent, "AbrirVentana_PedidoVI");
                oWnd.center();
                oWnd.Maximize();
            }


            function AbrirVentana_ProPedido_Internet(Id, guardar, modificar, eliminar, imprimir, Anio, semana) {
                //debugger;
                var oWnd = radopen("ProPedidoVI.aspx?IdPeInt=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana, "AbrirVentana_PedidoVI");
                oWnd.center();
                oWnd.Maximize();
            }
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                continuarAccion = true;

                switch (button.get_value()) {
                    case 'edit':
                        AbrirVentana_Pedido();
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function AbrirVentana_Pedido() {
                var oWnd = radopen("CapPedidoCaptado.aspx", "AbrirVentana_PedidoCaptado");
                //oWnd.center();
                oWnd.Maximize();
            }

            function AbrirProtocolos() {
                var oWnd = radopen("VentanaProtocolos.aspx", "AbrirVentana_Protocolos");
                oWnd.center();
            }
            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de Pedido se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_Pedido(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    ModificaBanderaRebind_Pedido('0');
                    //refreshGrid_FacturaPedido();  <---- se comentariza para abrir directamente la pantalla
                    //a diferencia de la facturacion de remisiones en la que si se requiere ir al servidor para validar
                    //la variable de sesion de facturacion de remisiones, aqui se invoca directamente la pantalla de 
                    //facturacion ya que en ella se valida directamente si el usuario eligio un pedido haciendo clic en el boton 'Aceptar'
                    //de la pantalla de 'factura pedido'. Si el pedido era válido, la variable de sesion de pedido trae un valor de lo cntrario en nula.
                    var oWnd = radopen("ProPedidoVI.aspx?IdVI=" + 0 + "&PermisoGuardar=" + true + "&PermisoModificar=" + true + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir=" + permisoImprimir + "&Anio=" + 0 + "&Semana=" + 0, "AbrirVentana_PedidoVI");
                    //oWnd.center();
                    oWnd.Maximize();
                }
            }
            function LimpiarBanderaRebind_Pedido(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind_Pedido('0');
            }

            function ActivarBanderaRebind_Pedido() {
                //debugger;
                ModificaBanderaRebind_Pedido('1');
            }

            function ModificaBanderaRebind_Pedido(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

            function resize() {
                var height = getDocHeight();
                var elements = window.top.document.getElementsByTagName("table");

                for (var i = 0; i < elements.length; i++) {
                    var containerPageViewID = "RadPageView1";

                    if (elements[i].id.indexOf(containerPageViewID) > -1) {
                        elements[i].style.height = height + "px";
                        break;
                    }
                }
            }

            if (window.addEventListener)
                window.addEventListener("load", resize, false);
            else if (window.attachEvent)
                window.attachEvent("onload", resize);
            else window.onload = resize;

            function getDocHeight() {
                var D = document;
                return Math.max(
        Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
        Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
        Math.max(D.body.clientHeight, D.documentElement.clientHeight)
    );
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

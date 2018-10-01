<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatTerritoriosActualiza.aspx.cs" Inherits="SIANWEB.CatTerritoriosActualiza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="cmbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rdActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="chkRetencion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkPorcientoIVA">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
            <telerik:AjaxSetting AjaxControlID="cmbCorporativa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgTerritorios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="pnlGrid" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
            <tr>
                <td valign="middle">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td valign="middle" style="text-align: right" width="150px">
                    <asp:Label ID="Label7" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td valign="middle" width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table id="TablaTerritorios" style="font-family: verdana; font-size: 8pt" runat="server" width="100%">
                                <tr>
                                    <td valign="middle">
                                            <telerik:RadGrid ID="rgTerritorios" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" CellSpacing="0" Culture="es-ES" GridLines="None" 
                                                Height="460px"  Width="100%"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                                OnItemCommand="rgTerritorios_ItemCommand" 
                                                OnNeedDataSource="rgTerritorios_NeedDataSource" PageSize="200" 
                                                onitemdatabound="rgTerritorios_ItemDataBound">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="True"/>
                                                </ClientSettings>
                                                <MasterTableView EditMode="InPlace">
                                                    <Columns>
                                                        
                                                        <%----------------------------------------------------------------%>
                                                        <telerik:GridTemplateColumn HeaderText="Nueva" UniqueName="Id_TerNuevo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblIdTerNue" runat="server" Text='<%# Bind("Id_TerNuevo") %>' Width="80px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_TerNue" runat="server" Enabled="False" Text='<%# Bind("Id_TerNuevo") %>' Width="80px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" 
                                                                        OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="80px" />
                                                        </telerik:GridTemplateColumn>

                                                        <%----------------------------------------------------------------%>
                                                        <telerik:GridTemplateColumn HeaderText="Actual" UniqueName="Id_Ter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblIdTer" runat="server" Text='<%# Bind("Id_Ter") %>' Width="60px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Ter" runat="server" Enabled="False" Text='<%# Bind("Id_Ter") %>' Width="60px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>
                                                        <telerik:GridTemplateColumn HeaderText="Anterior" UniqueName="Id_TerAnt" 
                                                            DataField="Id_TerAnt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblIdTerAnt" runat="server" Text='<%# Bind("Id_TerAnt") %>' Width="30px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_TerAnt" runat="server" Enabled="False" Text='<%# Bind("Id_TerAnt") %>' Width="30px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="30px" />
                                                        </telerik:GridTemplateColumn>
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Territorio" UniqueName="Territorio">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelDescTe" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbloldDescTe" runat="server" Text='<%# Bind("Descripcion") %>' 
                                                                    Visible="false" />
                                                                <telerik:RadTextBox ID="DescripcionTerr" runat="server" 
                                                                    Text='<%# Bind("Descripcion") %>'>
                                                                </telerik:RadTextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_TipoRepresentante">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LTipoRep" runat="server" Text='<%# Bind("Id_TipoRepresentante") %>' Width="20px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_TipoRep" runat="server" Enabled="false" Text='<%# Bind("Id_TipoRepresentante") %>' Width="20px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="20px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="TipoRep" 
                                                            UniqueName="TipoRepresentante_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblTipoRepNombre" runat="server" Text='<%# Bind("TipoRepresentante_Nombre") %>' Width="100px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblTipoRep" runat="server" 
                                                                    Text='<%# Bind("Id_TipoRepresentante") %>' Visible="false" />
                                                                <telerik:RadComboBox ID="cmbTipoRep" runat="server" AutoPostBack="true" 
                                                                    EmptyMessage="Seleccione Tipo Representante" Enable="false" 
                                                                    MarkFirstMatch="true" OnDataBinding="cmbTipoRep_DataBinding" 
                                                                    OnItemDataBound="TipoRep_ItemDataBound" OnTextChanged="cmbTipoRep_TextChanged" 
                                                                    Width="100px">
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Uen">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121Id_Uen" runat="server" Text='<%# Bind("Id_Uen") %>' Width="15px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Id_Uen" runat="server" Enabled="false" Text='<%# Bind("Id_Uen") %>' Width="20px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" 
                                                                        OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="20px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="UEN" UniqueName="Uen_Descripcion">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblUenNombre" runat="server" 
                                                                    Text='<%# Bind("Uen_Descripcion") %>' Width="170px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblUen" runat="server" Text='<%# Bind("Id_Uen") %>' 
                                                                    Visible="false" Width="170px" />
                                                                <telerik:RadComboBox ID="cmbUen" runat="server" AutoPostBack="true" 
                                                                    EmptyMessage="Seleccione UEN" Enable="false" MarkFirstMatch="true" 
                                                                    OnDataBinding="cmbUen_DataBinding" OnItemDataBound="Uen_ItemDataBound" 
                                                                    OnTextChanged="cmbUen_TextChanged" Width="170px">
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="170px" />
                                                        </telerik:GridTemplateColumn>
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Seg">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121Id_Seg" runat="server" Text='<%# Bind("Id_Seg") %>' Width="15px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Id_Seg" runat="server" Enabled="false" Text='<%# Bind("Id_Seg") %>' Width="20px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" 
                                                                        OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="20px" />
                                                        </telerik:GridTemplateColumn>

                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Segmento" UniqueName="Seg_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblSegNombre" runat="server" Text='<%# Bind("Seg_Nombre") %>' 
                                                                    Width="245px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblSeg" runat="server" Text='<%# Bind("Id_Seg") %>' 
                                                                    Visible="false" />
                                                                <telerik:RadComboBox ID="cmbSeg" runat="server" AutoPostBack="true" 
                                                                    EmptyMessage="Seleccione segmento" Enable="false" MarkFirstMatch="true" 
                                                                    OnDataBinding="cmbSegmento_DataBinding" 
                                                                    OnItemDataBound="Segmento_ItemDataBound" 
                                                                    OnTextChanged="cmbSegmento_TextChanged" Width="245px">
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="245px"/>
                                                        </telerik:GridTemplateColumn>
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Rik">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label121Id_Rik" runat="server" Text='<%# Bind("Id_Rik") %>' Width="40px" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Id_Rik" runat="server" Enabled="false" Text='<%# Bind("Id_Rik") %>' Width="40px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" 
                                                                        OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>

                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Representante" UniqueName="Rik_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblRikNombre" runat="server" Text='<%# Bind("Rik_Nombre") %>' Width="100px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblRik" runat="server" Text='<%# Bind("Id_Rik") %>' 
                                                                    Visible="false" />
                                                                <telerik:RadComboBox ID="cmbRik" runat="server" AutoPostBack="true" 
                                                                    EmptyMessage="Seleccione Representante" Enable="false" MarkFirstMatch="true" 
                                                                    OnDataBinding="cmbRik_DataBinding" OnItemDataBound="Rik_ItemDataBound" 
                                                                    OnTextChanged="cmbRik_TextChanged" Width="150px">
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_TipoCliente">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblId_TipoCliente" runat="server" Text='<%# Bind("Id_TipoCliente") %>' Width="15px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtId_Id_TipoCliente" runat="server" Enabled="false" Text='<%# Bind("Id_TipoCliente") %>' Width="20px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtTerritorioPartida_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="20px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>

                                                        <telerik:GridTemplateColumn HeaderText="Tipo Cliente" 
                                                            UniqueName="TipoCliente_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblTipoCliNombre" runat="server" Text='<%# Bind("TipoCliente_Nombre") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblTipoCli" runat="server" Text='<%# Bind("Id_TipoCliente") %>' Visible="false" />
                                                                <telerik:RadComboBox ID="cmbTipoCli" runat="server" AutoPostBack="true" 
                                                                    EmptyMessage="Seleccione Tipo Cliente" Enable="false" MarkFirstMatch="true" 
                                                                    OnDataBinding="cmbTipoCli_DataBinding" OnItemDataBound="TipoCli_ItemDataBound" 
                                                                    OnTextChanged="cmbTipoCli_TextChanged" Width="150px">
                                                                </telerik:RadComboBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <%----------------------------------------------------------------%>



                                                        <telerik:GridTemplateColumn HeaderText="Consecutivo" UniqueName="Consecutivo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelConsecutivo" runat="server" Text='<%# Bind("Consecutivo") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbloldConsecutivo" runat="server" Text='<%# Bind("Consecutivo") %>' Visible="false" />
                                                                
                                                                <telerik:RadTextBox  ID="Consecutivo" runat="server" Text='<%# Bind("Consecutivo") %>' MaxLength="15" AutoPostBack="true" ReadOnly>
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico"/>
                                                                </telerik:RadTextBox>



                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>





                                                        <telerik:GridTemplateColumn HeaderText="Id Local" UniqueName="Id_Local">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Id_Local") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblold5" runat="server" Text='<%# Bind("Id_Local") %>' Visible="false" />
                                                                
                                                                <telerik:RadTextBox  ID="Id_Local" runat="server" Text='<%# Bind("Id_Local") %>' MaxLength="15" OnTextChanged="RDId_Local" AutoPostBack="true">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico"/>
                                                                </telerik:RadTextBox>



                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridTemplateColumn>



                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" 
                                                            EditText="Editar" InsertText="Aceptar" UniqueName="EditCommandColumn" 
                                                            UpdateText="Aceptar">
                                                            <HeaderStyle Width="40px"/>
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="40px"/>
                                                        </telerik:GridEditCommandColumn>
                                                      
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn UniqueName="EditCommandColumn1">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" 
                                                    NextPagesToolTip="Páginas siguientes" NextPageToolTip="Página siguiente" 
                                                    PageButtonCount="3" 
                                                    PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;." 
                                                    PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores" 
                                                    PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                    </td>
                                </tr>
                     </table>

    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirVentana_Autorizacion() {
                var oWnd = radopen("Ventana_Autorizacion.aspx", "AbrirVentana_Autorizacion");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }
            function autorizar(id_u, id_cd, nombre) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(id_u + "@" + id_cd + "@" + nombre);
            }


            // ----------------------
            // INICIO - TERRITORIO 
            // ----------------------
            var cmbter;
            var txtTerritorioPartida;
            
            function txtTerritorioPartida_OnLoad(sender, args) {
                //txtTerritorioPartida = sender;
            }

            function txtTerritorioPartida_OnBlur(sender, args) {
                OnBlur(sender, cmbter); 
            }

            function cmbTerritorioPartida_OnLoad(sender, args) 
            {cmbTer = sender;}

            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs)
            { ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida); }

            // ----------------------
            // FIN - TERRITORIO 
            // ----------------------

            // ----------------------
            // INICIO - TIPO DE TERRITORIO 
            // ----------------------

            var cmbTipoter;
            var txtTipoTerritorioPartida;

            function txtTipoTerritorioPartida_OnLoad(sender, args)
            { txtTipoTerritorioPartida = sender; }

            function txtTipoTerritorioPartida_OnBlur(sender, args)
            { OnBlur(sender, cmbTipoter); }

            function cmbTipoTerritorioPartida_OnLoad(sender, args) 
            {cmbTipoter = sender;}

            function cmbTipoTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs)
            { ClientSelectedIndexChanged(eventArgs.get_item(), txtTipoTerritorioPartida); }

            // ----------------------
            // FIN - TIPO DE TERRITORIO 
            // ----------------------






            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;

                var button = args.get_item();
                
                if (txtClave.get_value() == "" || txtDescripcion.get_value() == "" || numCorporativo.get_value() == "" || txtFcalle.get_value() == "" || txtFnumero.get_value() == "" ||
                txtFcp.get_value() == "" || txtFcolonia.get_value() == "" || txtFmunicipio.get_value() == "" || txtFestado.get_value() == "" || txtFrfc.get_value() == "") {
                    radTabStrip.get_allTabs()[0].select();
                }
                else if (cmb.get_value() == "" || cmb.get_value() == "-1") {
                    radTabStrip.get_allTabs()[2].select();
                }
                else {
                    radTabStrip.get_allTabs()[0].select();
                }
            }

            //--------------------------------------------------------------------------------------------------
            //   controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
            }


            var txtDimension;
            var txtPesos;
            var txtPotencial;

            var txtUen;


            function OnUenLoad(sender, args) {
                txtUen = sender;
            }


        </script>
    </telerik:RadCodeBlock>
</asp:Content>

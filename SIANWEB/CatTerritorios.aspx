<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatTerritorios.aspx.cs" Inherits="SIANWEB.CatTerritorios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbUen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                       />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
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
                    ImageUrl="~/Imagenes/blank.png"/>
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
                    <asp:Label ID="Label5" runat="server" Text="Centro de distribución"></asp:Label>
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
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talles" AccessKey="E" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%"
                        BorderStyle="Solid" BorderWidth="1px" Width="950px">
                        
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Width="100%">

                            <asp:Table ID="tblTipoRepresentante" runat="server" />
                            <asp:HiddenField ID="hfTipoRepresentante" runat="server" Value="" />

                            <%--<table style="display:none">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">
                                        <asp:RadioButton ID="chkRSC" runat="server" Text="RSC" GroupName="Tipos" OnCheckedChanged="chkRSC_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">
                                        <asp:RadioButton ID="chkAsesor" runat="server" Text="Asesor" GroupName="Tipos" OnCheckedChanged="chkAsesor_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">
                                        <asp:RadioButton ID="chkRIK" runat="server" Text="RIK" GroupName="Tipos" OnCheckedChanged="chkRIK_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">
                                        <asp:RadioButton ID="chkOTRO" runat="server" Text="GERENCIAL" GroupName="Tipos" OnCheckedChanged="chkOTRO_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                            
                            <table id="DetalleTipoRep">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">&nbsp;</td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="70">&nbsp;</td>
                                    <td width="123">&nbsp;</td>
                                    <td width="75">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUen" runat="server" Text="UEN"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUen" runat="server" MaxLength="9" 
                                            MinValue="1" Width="70px" AutoPostBack="True" 
                                            ontextchanged="txtUen_TextChanged">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt1_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbUen" runat="server" AutoPostBack="True" 
                                            ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" 
                                            DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains" 
                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" 
                                            OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged" 
                                            onselectedindexchanged="cmbUen_SelectedIndexChanged" Width="250px">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID0" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                                Width="50px" />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC0" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="ReqFieldUEN" runat="server" 
                                            ControlToValidate="cmbUen" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSegmento" runat="server" Text="Segmento / Giro"></asp:Label>
                                    </td>
                                    <td >
                                        <telerik:RadNumericTextBox ID="txtSegmento" runat="server" MaxLength="9" 
                                            MinValue="1" Width="70px" AutoPostBack="True" >
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt3_OnBlur" />
                                        </telerik:RadNumericTextBox></td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbSegmento" runat="server" 
                                            ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" 
                                            DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains" 
                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" 
                                            onchanged="txtSegmento_TextChanged"
                                            OnClientSelectedIndexChanged="cmb3_ClientSelectedIndexChanged" Width="250px">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID1" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                                Width="50px" />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC1" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;<asp:RequiredFieldValidator ID="ReqFieldSegmento" runat="server" 
                                            ControlToValidate="cmbUen" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoCliente" runat="server" Text="Tipo de Cliente"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTipoCliente" runat="server" MaxLength="9" 
                                            MinValue="1" Width="70px" AutoPostBack="True" 
                                            ontextchanged="txtTipoCliente_TextChanged">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txtTipoCliente_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbTipoCliente" runat="server" 
                                            ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" 
                                            DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains" 
                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" 
                                            OnClientSelectedIndexChanged="cmbTipoClientSelectedIndexChanged" Width="250px">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID2" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                                Width="50px" />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC2" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="ReqFieldTipoCliente" runat="server" 
                                            ControlToValidate="cmbUen" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCveTerritorio" runat="server" Text="Clave Territorio"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadNumericTextBox ID="txtClave" runat="server" MaxLength="9" 
                                            MinValue="1" Width="70px" Visible="False" ReadOnly="True">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td width="70">
                                        &nbsp;</td>
                                    <td width="123">
                                        &nbsp;</td>
                                    <td width="75">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblidLocal" runat="server" Text="Id. Local"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox onpaste="return false" ID="txtidLocal" runat="server" Width="100px">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="ReqFieldIdLocal" runat="server" 
                                            ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                      &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td width="70">
                                        &nbsp;</td>
                                    <td width="123">
                                       <asp:Label ID="LabelTerritorioAutorizado" runat="server" Text="Territorio Autorizado"></asp:Label>  &nbsp;</td>
                                    <td width="75">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                         <td>
                                      &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td width="123">
                                       <asp:Label ID="LabelSolicitudCambio" runat="server" Text="Solicitud de Cambio"></asp:Label> &nbsp;</td>
                                    <td width="123">
                                        <asp:Label ID="Label6" runat="server" Text="#:"></asp:Label>&nbsp; <asp:Label ID="LblIdSolicitud" runat="server" Text=""></asp:Label></td>
                                    <td width="75">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                              
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRik" runat="server" Text="Representante"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRik" ReadOnly="True" Enabled="false" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt2_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbRik" runat="server" ReadOnly="True" Enabled="false" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="ReqFieldRepresentante" runat="server" 
                                            ControlToValidate="cmbUen" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        &nbsp;
                                    </td>
                                    <!--- Aqui es la solicitud de cambio territorio REPRESENTANTE !-->

                                    <td >
                                     <asp:Label ID="lblRikSolicitud" runat="server" Text="Representante"></asp:Label>
                                    </td>
                                     <td>
                                        <telerik:RadNumericTextBox ID="txtRikSolicitud" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt2Solicitud_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                     <td colspan="2">
                                        <telerik:RadComboBox ID="cmbRikSolicitud" runat="server" OnClientSelectedIndexChanged="cmb2Solicitud_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="Label7" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                     <!--- FIN solicitud de cambio territorio REPRESENTANTE !-->
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtDescripcion" ReadOnly="True" Enabled="false" runat="server" MaxLength="50" 
                                            onpaste="return false" Width="197px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="ReqFieldTerritorio" runat="server" 
                                            ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <!-- Aqui empiezan los datos de solicitud territorio !-->
                                    
                                    <td>
                                        <asp:Label ID="lblTerritorioSolicitud" runat="server" Text="Territorio"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtDescripcionSolicitud" runat="server" MaxLength="50" 
                                            onpaste="return false" Width="197px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <!-- FIN datos de solicitud territorio !-->
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="HF_ID" runat="server" />
                                    </td>
                                    <td id="tdchkActivo">
                                        <asp:CheckBox ID="chkActivo" ReadOnly="True" Enabled="false" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                            OnCheckedChanged="chkActivo_CheckedChanged" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <!-- Aqui comienza el fin de la solictud de territorio -->
                                     <td>
                                        <asp:HiddenField ID="HF_IDSolicitud" runat="server" />
                                    </td>
                                    <td id="td1">
                                        <asp:CheckBox ID="chkActivoSolicitud" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                            OnCheckedChanged="chkActivo_CheckedChanged" />
                                    </td>
                                      <!--Fin de la solictud de territorio -->
                                </tr>
                                <tr>
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                        </telerik:RadPageView>
                        

                      


                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            OnNeedDataSource="rgDet_NeedDataSource" OnItemCommand="rgDet_ItemCommand" OnPageIndexChanged="rgDet_PageIndexChanged"
                                            Height="206px" PageSize="5" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" />
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros.">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Año" UniqueName="Anyo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Anyo") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold1" runat="server" Text='<%# Bind("Anyo") %>' Visible="false" />
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" MaxLength="4" Text='<%# Bind("Anyo") %>'
                                                                MinValue="1900">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Mes" UniqueName="Mes">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("MesStr") %>' />
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Mes") %>' Visible="false" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold2" runat="server" Text='<%# Bind("Mes") %>' Visible="false" />
                                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" OnDataBinding="RadComboBox_DataBinding"
                                                                SelectedValue='<%# Bind("Mes") %>' />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="180px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Presupuesto" UniqueName="Presupuesto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Presupuesto","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold3" runat="server" Text='<%# Bind("Presupuesto") %>' Visible="false" />
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Text='<%# Bind("Presupuesto") %>'
                                                                MinValue="0" MaxLength="9">             
                                                                 <NumberFormat DecimalDigits="2" GroupSeparator="" />                                                   
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UpdateText="Aceptar" EditText="Editar"
                                                        UniqueName="EditCommandColumn" CancelText="Cancelar" InsertText="Aceptar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                ShowPagerText="True" PageButtonCount="3" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" 
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    CellSpacing="0" Culture="es-ES">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Clave" UniqueName="Id_Ter">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="Descripcion">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_UEN" HeaderText="Id_UEN" 
                                                UniqueName="Id_UEN" Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Uen_Descripcion" 
                                                FilterControlAltText="Filter Uen_Descripcion column" HeaderText="UEN" 
                                                UniqueName="Uen_Descripcion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rik" HeaderText="Id_Rik" UniqueName="Id_Rik"
                                                Visible="True" Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rik_Nombre" 
                                                FilterControlAltText="Filter Rik_Nombre column" HeaderText="Representante" 
                                                UniqueName="Rik_Nombre">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="Id_Seg" UniqueName="Id_Seg"
                                                Visible="True" Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Seg_Nombre" 
                                                FilterControlAltText="Filter Seg_Nombre column" HeaderText="Segmento" 
                                                UniqueName="Seg_Nombre">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_TipoCliente" HeaderText="Id_TipoCliente" 
                                                UniqueName="Id_TipoCliente" 
                                                FilterControlAltText="Filter Id_TipoCliente column" Display="False" 
                                                ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoCliente_Nombre" 
                                                FilterControlAltText="Filter TipoCliente_Nombre column" 
                                                HeaderText="Tipo Cliente" UniqueName="TipoCliente_Nombre">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Local" 
                                                FilterControlAltText="Filter Id_Local column" HeaderText="Id. Local" 
                                                UniqueName="Id_Local" Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_TipoRepresentante" 
                                                FilterControlAltText="Filter Id_TipoRepresentante column" 
                                                HeaderText="Id_TipoRepresentante" UniqueName="Id_TipoRepresentante" 
                                                Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoRepresentante_Nombre" 
                                                FilterControlAltText="Filter TipoRepresentante_Nombre column" 
                                                HeaderText="Tipo Representante" UniqueName="TipoRepresentante_Nombre">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" 
                                                FilterControlAltText="Filter Estatus column" HeaderText="Estatus" 
                                                UniqueName="Estatus" Display="False" ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //            function AbrirVentana_CapturaRSCAsesor(Id_Territorio) 
            //            {
            //                var oWnd = radopen("Ventana_RSCAsesor.aspx?Id_Terr=" + Id_Territorio, "AbrirVentana_CapturaRSCAsesor");
            //                oWnd.set_showOnTopWhenMaximized(false);
            //                oWnd.center();
            //            }

            //            function AbrirVentana_CapturaRSCAsesor() 
            //            {
            //                var oWnd = radopen("Ventana_RSCAsesor.aspx", "AbrirVentana_CapturaRSCAsesor");
            //                oWnd.set_showOnTopWhenMaximized(false);
            //                oWnd.center();
            //            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

                //                debugger;

                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtUen.ClientID %>'));
                LimpiarTextBox($find('<%= txtRik.ClientID %>'));
                LimpiarTextBox($find('<%= txtSegmento.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbUen.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbRik.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSegmento.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbTipoCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtTipoCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtidLocal.ClientID %>'));

                LimpiarTextBox($find('<%= txtRikSolicitud.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);

                LimpiarComboSelectIndex0($find('<%= cmbRikSolicitud.ClientID %>'));
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                debugger;

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();


                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }


                switch (button.get_value()) {
                    case 'new':
                        debugger;

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';


                        var txtIdPrecio = $find('<%= txtClave.ClientID %>');
                        txtIdPrecio.enable();
                        txtIdPrecio.focus();
                        txtIdPrecio.set_value('<%= Valor %>');
                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbUen.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtUen.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
            }

            function txt2Solicitud_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRikSolicitud.ClientID %>'));
            }



            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRik.ClientID %>'));
                //  ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRikSolicitud.ClientID %>'));
            }

            function cmb2Solicitud_ClientSelectedIndexChanged(sender, eventArgs) {
                //ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRik.ClientID %>'));
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRikSolicitud.ClientID %>'));
            }


            function txt3_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbSegmento.ClientID %>'));
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtSegmento.ClientID %>'));
            }

            function cmbTipoClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoCliente.ClientID %>'));
            }

            function txtTipoCliente_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoCliente.ClientID %>'));
            }

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
                console.log($telerik.$("#<%=HF_ID.ClientID %>").val());
                if ($telerik.$("#<%=HF_ID.ClientID %>").val() !== '') {
                    console.log("span[Id_TipoRep='" + $telerik.$("#<%=hfTipoRepresentante.ClientID %>").val() + "'] > input");
                    $telerik.$("input[type=radio]").each(function () { $telerik.$(this).attr('disabled', true); });
                    $telerik.$("span[Id_TipoRep='" + $telerik.$("#<%=hfTipoRepresentante.ClientID %>").val() + "'] > input").each(function () {
                        console.log(this);
                        $telerik.$(this).attr("checked", true);
                    });
                } else {
                    $telerik.$("input[type=radio]").each(function () { $telerik.$(this).attr('disabled', false); });
                }

            });
            function window_onload() {

            }

        </script>
    </telerik:RadCodeBlock>
<script language="javascript" type="text/javascript" for="window" event="onload">
// <![CDATA[
return window_onload()
// ]]>
</script>
</asp:Content>

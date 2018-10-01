<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ConfiguracionCobranza.aspx.cs" Inherits="SIANWEB.ConfiguracionCobranza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .cssOcultar
        {
            display: none;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAcciones" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTPregunta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAcciones" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregar0">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAcciones" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="CheckAlmCob">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divProceso" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="CheckEmbAlm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divProceso" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CheckEntAlm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divProceso" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="BtnAgregarCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divCredito" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregar33">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAcciones" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRespuestas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRespuestas" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAcciones" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregarAlertas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAlertas" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAlertas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divAlertas" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregarGracia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGracia" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgGracia" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgGracia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGracia" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
        </Items>
    </telerik:RadToolBar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;" width="900px">
            <tr>
                <td width="10px">
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelected="tab_selected">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Acciones" PageViewID="RPVAcciones"
                             Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Alertas" PageViewID="RPVAlertas">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Reglas" PageViewID="RPVReglas">
                            </telerik:RadTab>    
                             <telerik:RadTab runat="server" Text="Proceso" PageViewID="RPVProceso" 
                               >
                            </telerik:RadTab>  
                              <telerik:RadTab runat="server" Text="Modificar crédito" PageViewID="RPVCredito">
                            </telerik:RadTab>                      
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="RPVAcciones" runat="server"><div runat="server" id="divAcciones"><table><tr><td width="10"></td><td width="110">&nbsp;&nbsp;</td><td><telerik:RadTextBox ID="txtGUIDAccion" runat="server" ReadOnly="true" Visible="false"></telerik:RadTextBox></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:Label ID="lblATipo" runat="server" Text="Etapa"></asp:Label></td><td><telerik:RadComboBox ID="cmbATipo" runat="server"><Items></Items></telerik:RadComboBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbATipo"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaAccion"
                                                InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator></td><td><asp:Label ID="lblADias" runat="server" Text="Días"></asp:Label></td><td><telerik:RadNumericTextBox ID="txtADias" runat="server" MaxLength="9" MinValue="0"
                                                Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtADias"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaAccion"></asp:RequiredFieldValidator></td></tr></table><table><tr><td width="10"></td><td width="110"><asp:Label ID="lblTPregunta" runat="server" Text="Tipo de respuesta"></asp:Label></td><td><telerik:RadComboBox ID="cmbTPregunta" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbTPregunta_SelectedIndexChanged"><Items><telerik:RadComboBoxItem runat="server" Owner="cmbTPregunta" Text="Abierta" Value="A" /><telerik:RadComboBoxItem runat="server" Owner="cmbTPregunta" Text="Múltiple" Value="M" /></Items></telerik:RadComboBox></td><td></td><td></td></tr><tr><td></td><td><asp:Label ID="lblPregunta" runat="server" Text="Pregunta"></asp:Label></td><td><telerik:RadTextBox ID="txtPregunta" runat="server" Width="250px"><ClientEvents OnKeyPress="SinComillas" /></telerik:RadTextBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPregunta"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaAccion"></asp:RequiredFieldValidator></td><td><asp:ImageButton 
                                        ID="imgAgregar0" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                                OnClick="imgAgregar_Click" ToolTip="Agregar" 
                                        ValidationGroup="AgregaAccion" Height="16px" Width="16px" /><asp:ImageButton ID="imgAgregar33" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                                OnClick="imgAgregar_Click" ToolTip="Agregar" ValidationGroup="AgregaAccion" Visible="False" /></td></tr></table><table><tr id="trRespuestas" runat="server" visible="false"><td></td><td valign="top"><asp:Label ID="lblRespuestas" runat="server" Text="Respuestas"></asp:Label></td><td><telerik:RadGrid ID="rgRespuestas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgRespuestas_ItemCommand"
                                                OnNeedDataSource="rgRespuestas_NeedDataSource" OnPageIndexChanged="rgRespuestas_PageIndexChanged"
                                                Height="200px"><ClientSettings><Scrolling AllowScroll="True" ScrollHeight="200px" UseStaticHeaders="false" /></ClientSettings><MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros."
                                                    TableLayout="Auto"><CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" /><Columns><telerik:GridBoundColumn HeaderText="IdStr" UniqueName="IdStr" DataField="IdStr"
                                                            ReadOnly="true" Display="false"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridTemplateColumn DataField="Descripcion" HeaderText="Respuesta" UniqueName="Respuesta"><HeaderStyle HorizontalAlign="Center" Width="300px" /><ItemTemplate><asp:Label ID="lblRespuesta" runat="server" Text='<%# Eval("Descripcion").ToString() %>' /></ItemTemplate><EditItemTemplate><telerik:RadTextBox ID="txtRespuesta" runat="server" MaxLength="100" Text='<%# Eval("Descripcion").ToString() %>'
                                                                    Width="100%"><ClientEvents OnKeyPress="SinComillas" /></telerik:RadTextBox></EditItemTemplate></telerik:GridTemplateColumn><telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditImageUrl="~/Imagenes/blank.png"
                                                            EditText="Editar" InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar"><HeaderStyle Width="70px" /><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" /></telerik:GridEditCommandColumn><telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar"><HeaderStyle Width="50px" /><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                Width="30px" /></telerik:GridButtonColumn></Columns></MasterTableView><PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                                    NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                                    PrevPageToolTip="Página anterior" ShowPagerText="True" /></telerik:RadGrid></td><td valign="top">&nbsp;&nbsp;</td><td valign="top"></td></tr><tr><td width="10"></td><td width="110"></td><td></td><td></td><td></td></tr></table><table><tr><td width="10px"></td><td><telerik:RadGrid ID="rgAcciones" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgAcciones_ItemCommand"
                                                OnNeedDataSource="rgAcciones_NeedDataSource" OnPageIndexChanged="rgAcciones_PageIndexChanged"><ClientSettings><Scrolling AllowScroll="True" ScrollHeight="250px" UseStaticHeaders="true" /></ClientSettings><MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None"
                                                    TableLayout="Auto"><CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" /><Columns><telerik:GridBoundColumn HeaderText="GUID" UniqueName="GUID" DataField="GUID" Display="False"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Etapa" UniqueName="EtapaStr" DataField="EtapaStr"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Días" UniqueName="Dias" DataField="Dias"><HeaderStyle Width="50px" /><ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Tipo de respuesta" UniqueName="Tipo_RespuestaStr"
                                                            DataField="Tipo_RespuestaStr"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Pregunta" UniqueName="Pregunta" DataField="Pregunta"><HeaderStyle Width="240px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Respuestas" UniqueName="RespuestasStr" DataField="RespuestasStr"><HeaderStyle Width="200px" /></telerik:GridBoundColumn><telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" /></ItemTemplate><HeaderStyle Width="50px"></HeaderStyle><ItemStyle HorizontalAlign="Center"></ItemStyle></telerik:GridTemplateColumn><telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar"><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" /></telerik:GridButtonColumn></Columns><HeaderStyle HorizontalAlign="Center" /></MasterTableView><PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" /></telerik:RadGrid></td><td width="10px"></td></tr><tr><td width="10px"></td><td></td><td width="10px"></td></tr></table></div></telerik:RadPageView>
                        <telerik:RadPageView ID="RPVAlertas" runat="server"><div runat="server" id="divAlertas"><table><tr><td width="10"></td><td width="60">&nbsp;&nbsp;</td><td><telerik:RadTextBox ID="txtGUIDAlerta" runat="server" ReadOnly="true" Visible="false"></telerik:RadTextBox></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:Label ID="lblAlTipo" runat="server" Text="Etapa"></asp:Label></td><td><telerik:RadComboBox ID="cmbAlTipo" runat="server" OnClientSelectedIndexChanged="combo_SelectedChanged"><Items></Items></telerik:RadComboBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmbAlTipo"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                ValidationGroup="AgregaAlerta"></asp:RequiredFieldValidator></td><td><table><tr id="trAlertaDias"><td><asp:Label ID="lblAlDias" runat="server" Text="Días"></asp:Label></td><td><telerik:RadNumericTextBox ID="txtAlDias" runat="server" MaxLength="9" MinValue="0"
                                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAlDias"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaAlerta"></asp:RequiredFieldValidator></td></tr></table></td></tr></table><table><tr><td width="10"></td><td width="60"><asp:Label ID="lblEnviar" runat="server" Text="Enviar a"></asp:Label></td><td><telerik:RadComboBox ID="cmbEnviar" runat="server" Width="200px"><Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" /><telerik:RadComboBoxItem runat="server" Owner="cmbEnviar" Text="Gestor de cobranza"
                                                        Value="Gestor de cobranza" /><telerik:RadComboBoxItem runat="server" Owner="cmbEnviar" Text="Rik" Value="Rik" /><telerik:RadComboBoxItem runat="server" Owner="cmbEnviar" Text="Gerente de centro de distribución"
                                                        Value="Gerente de centro de distribución" /></Items></telerik:RadComboBox></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmbEnviar"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                ValidationGroup="AgregaAlerta"></asp:RequiredFieldValidator></td><td><asp:ImageButton ID="imgAgregarAlertas" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                                OnClick="imgAgregarAlertas_Click" ToolTip="Agregar" ValidationGroup="AgregaAlerta" /></td><td></td></tr><tr id="trAlertas" runat="server" visible="false"><td></td><td width="60"></td><td><asp:CheckBox ID="chkSuspender" runat="server" Text="Suspender crédito" /></td><td></td><td></td><td></td></tr></table><table><tr><td width="10px"></td><td><telerik:RadGrid ID="rgAlertas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgAlertas_ItemCommand"
                                                OnNeedDataSource="rgAlertas_NeedDataSource" OnPageIndexChanged="rgAlertas_PageIndexChanged"><ClientSettings><Scrolling AllowScroll="True" ScrollHeight="250px" UseStaticHeaders="true" SaveScrollPosition="true" /></ClientSettings><MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None"
                                                    TableLayout="Auto"><CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" /><Columns><telerik:GridBoundColumn HeaderText="GUID" UniqueName="GUID" DataField="GUID" Display="False"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Etapa" UniqueName="EtapaStr" DataField="EtapaStr"><HeaderStyle Width="200px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Días" UniqueName="Dias" DataField="Dias"><HeaderStyle Width="50px" /><ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Enviar a" UniqueName="EnviarA" DataField="EnviarAStr"><HeaderStyle Width="270px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Suspender credito" UniqueName="SuspenderCreditoStr"
                                                            DataField="SuspenderCreditoStr" Display="false"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle><ItemStyle HorizontalAlign="Center"></ItemStyle></telerik:GridTemplateColumn><telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar"><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" /></telerik:GridButtonColumn></Columns><HeaderStyle HorizontalAlign="Center" /></MasterTableView><PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" /></telerik:RadGrid></td><td width="210px"></td></tr><tr><td width="10px"></td><td></td><td></td></tr></table></div></telerik:RadPageView>
                        <telerik:RadPageView ID="RPVReglas" runat="server"><div runat="server" id="divGracia" visible="True"><table><tr><td width="10">&nbsp;&nbsp;</td><td><telerik:RadTextBox ID="txtGUIDGracia" runat="server" ReadOnly="true" Visible="false"
                                                Width="50px"></telerik:RadTextBox></td><td width="5">&nbsp;&nbsp;</td><td></td><td width="5">&nbsp;&nbsp;</td><td width="30"></td><td></td><td></td></tr><tr><td width="10">&nbsp;&nbsp;</td><td colspan="7" width="130"><asp:Label ID="lblPeriodoGracia" runat="server" Font-Bold="True" Text="Periodo de gracia"></asp:Label></td></tr><tr><td></td><td><asp:Label ID="lblCondiciones" runat="server" Text="Condiciones de pago"></asp:Label></td><td>&nbsp;&nbsp;</td><td><telerik:RadNumericTextBox ID="TxtCondiciones" runat="server" MaxLength="9" MinValue="1"
                                                Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td>&nbsp;&nbsp;</td><td><asp:Label ID="lblDias4" runat="server" Text="días"></asp:Label></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtCondiciones"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaGracia"></asp:RequiredFieldValidator></td><td></td></tr><tr><td></td><td><asp:Label ID="lblPlazoGracia" runat="server" Text="Periodo de gracia"></asp:Label></td><td>&nbsp;&nbsp;</td><td><telerik:RadNumericTextBox ID="TxtPeriodoGracia" runat="server" MaxLength="9" MinValue="1"
                                                Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td>&nbsp;&nbsp;</td><td><asp:Label ID="lblDias5" runat="server" Text="días"></asp:Label></td><td><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TxtPeriodoGracia"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="AgregaGracia"></asp:RequiredFieldValidator></td><td><asp:ImageButton ID="imgAgregarGracia" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                                OnClick="imgAgregarGracia_Click" ToolTip="Agregar" ValidationGroup="AgregaGracia" /></td></tr></table><table><tr><td width="10"></td><td><telerik:RadGrid ID="rgGracia" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgGracia_ItemCommand"
                                                OnNeedDataSource="rgGracia_NeedDataSource"><ClientSettings><Scrolling AllowScroll="True" ScrollHeight="150px" UseStaticHeaders="true" /></ClientSettings><MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None"
                                                    TableLayout="Auto"><CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" /><Columns><telerik:GridBoundColumn HeaderText="GUID" UniqueName="GUID" DataField="GUID" Display="False"><HeaderStyle Width="120px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Condiciones de pago" UniqueName="Reg_Condicion"
                                                            DataField="Reg_Condicion"><HeaderStyle Width="150" /><ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Periodo de gracia" UniqueName="Reg_Periodo"
                                                            DataField="Reg_Periodo"><HeaderStyle Width="150" /><ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn><telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle><ItemStyle HorizontalAlign="Center"></ItemStyle></telerik:GridTemplateColumn><telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar"><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" /></telerik:GridButtonColumn></Columns><HeaderStyle HorizontalAlign="Center" /></MasterTableView><PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" /></telerik:RadGrid></td><td width="430"></td></tr></table></div><table runat="server" id="table1" visible="False"><tr><td width="10px"></td><td width="200"></td><td></td><td></td></tr><tr><td width="10px"></td><td><asp:Label ID="lblPlazo" runat="server" Text="Periodo de gracia"></asp:Label></td><td><telerik:RadNumericTextBox ID="TxtPlazo" runat="server" MaxLength="9" MinValue="0"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td><asp:Label ID="lblDias7" runat="server" Text="días"></asp:Label></td></tr></table><table runat="server" id="cobranza"><tr><td width="10px"></td><td height="10"></td><td width="3"></td><td></td><td width="3"></td><td></td><td width="3"></td><td></td><td width="3"></td><td></td><td width="3"></td><td></td><td></td><td></td></tr><tr><td width="10px"></td><td colspan="11"><asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Autorización de condiciones de pago especiales"></asp:Label></td><td></td><td></td></tr><tr><td width="10px"></td><td><asp:Label ID="lblAutoriza1" runat="server" Text="Autoriza"></asp:Label></td><td></td><td><telerik:RadComboBox ID="cmbAutoriza1" runat="server" Width="200px"><Items><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza1" Text="-- Seleccionar --"
                                                    Value="-- Seleccionar --" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza1" Text="Gestor de cobranza"
                                                    Value="Gestor de cobranza" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza1" Text="Rik" Value="Rik" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza1" Text="Gerente de centro de distribución"
                                                    Value="Gerente de centro de distribución" /></Items></telerik:RadComboBox></td><td></td><td><asp:Label ID="lblde1" runat="server" Text="de"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias1" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango1" OnLoad="D1_Load" /></telerik:RadNumericTextBox></td><td></td><td><asp:Label ID="lbla1" runat="server" Text="a"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias2" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango1_2" OnLoad="D2_Load" /></telerik:RadNumericTextBox></td><td><asp:Label ID="lblDias1" runat="server" Text="días"></asp:Label></td><td></td></tr><tr><td width="10px"></td><td><asp:Label ID="lblAutoriza2" runat="server" Text="Autoriza"></asp:Label></td><td></td><td><telerik:RadComboBox ID="cmbAutoriza2" runat="server" Width="200px"><Items><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza2" Text="-- Seleccionar --"
                                                    Value="-- Seleccionar --" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza2" Text="Gestor de cobranza"
                                                    Value="Gestor de cobranza" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza2" Text="Rik" Value="Rik" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza2" Text="Gerente de centro de distribución"
                                                    Value="Gerente de centro de distribución" /></Items></telerik:RadComboBox></td><td></td><td><asp:Label ID="lblde2" runat="server" Text="de"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias3" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango2" OnLoad="D3_Load" /></telerik:RadNumericTextBox></td><td></td><td><asp:Label ID="lbla2" runat="server" Text="a"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias4" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango2_2" OnLoad="D4_Load" /></telerik:RadNumericTextBox></td><td><asp:Label ID="lblDias2" runat="server" Text="días"></asp:Label></td><td></td></tr><tr><td width="10px"></td><td><asp:Label ID="lblAutoriza3" runat="server" Text="Autoriza"></asp:Label></td><td></td><td><telerik:RadComboBox ID="cmbAutoriza3" runat="server" Width="200px"><Items><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza3" Text="-- Seleccionar --"
                                                    Value="-- Seleccionar --" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza3" Text="Gestor de cobranza"
                                                    Value="Gestor de cobranza" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza3" Text="Rik" Value="Rik" /><telerik:RadComboBoxItem runat="server" Owner="cmbAutoriza3" Text="Gerente de centro de distribución"
                                                    Value="Gerente de centro de distribución" /></Items></telerik:RadComboBox></td><td></td><td><asp:Label ID="lblde3" runat="server" Text="de"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias5" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango3" OnLoad="D5_Load" /></telerik:RadNumericTextBox></td><td></td><td><asp:Label ID="lbla3" runat="server" Text="a"></asp:Label></td><td></td><td><telerik:RadNumericTextBox ID="txtDias6" runat="server" MaxLength="9" MinValue="1"
                                            Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /><ClientEvents OnBlur="txt_rango3_2" OnLoad="D6_Load" /></telerik:RadNumericTextBox></td><td><asp:Label ID="lblDias3" runat="server" Text="días"></asp:Label></td><td></td></tr><tr><td width="10px"></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></table></telerik:RadPageView>
                         <telerik:RadPageView ID="RPVProceso" runat="server"><div runat="server" id="divProceso" visible="True"><table><tr><td width="10px">&nbsp;&nbsp;</td><td><asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Entrega de Mercancia"></asp:Label></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td><asp:CheckBox ID="CheckSvtasAlm" runat="server" Text="Relación de Facturas Enviadas de Servicio de Ventas a Almacén" /></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>
                             <asp:CheckBox ID="CheckEmbAlm" runat="server" Text="Confirmación de Embarques" 
                                 AutoPostBack="True" oncheckedchanged="CheckEmbAlm_CheckedChanged" /></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>
                                 <asp:CheckBox ID="CheckEntAlm" runat="server" Text="Confirmación de Entregas" 
                                     AutoPostBack="True" oncheckedchanged="CheckEntAlm_CheckedChanged" /></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td><asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Entrega Cobranza"></asp:Label></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>
                             <asp:CheckBox ID="CheckAlmCob" runat="server" 
                                 Text="Relación de Facturas Enviadas de Almacén a Cobranza" AutoPostBack="True" 
                                 oncheckedchanged="CheckAlmCob_CheckedChanged" /></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td><asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Revisión"></asp:Label></td></tr><tr><td width="10px">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td><asp:CheckBox ID="CheckRevCob" runat="server" Text="Relación de Facturas Enviadas a Revisión ó Cobro" /></td></tr></table></div></telerik:RadPageView>
                        <telerik:RadPageView ID="RPVCredito" runat="server"><div runat="server" id="divCredito" visible="True"><table><tr><td width="10"></td><td width="60">&nbsp;&nbsp;</td><td><telerik:RadTextBox ID="RadTextBox1" runat="server" ReadOnly="true" Visible="false"></telerik:RadTextBox></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:Label ID="Label5" runat="server" Text="Tipo de usuario" Width="150px"></asp:Label></td><td><telerik:RadComboBox ID="CmbTipoU" Width="150px" runat="server" OnClientSelectedIndexChanged="combo_SelectedChanged"></telerik:RadComboBox></td><td><asp:RequiredFieldValidator ID="RfvTipoU" runat="server" ControlToValidate="CmbTipoU"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                ValidationGroup="AgregaCuenta"></asp:RequiredFieldValidator></td><td><table><tr id="tr1"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table></td></tr><tr><td></td><td><asp:Label ID="Label9" runat="server" Text="Días"></asp:Label></td><td><telerik:RadNumericTextBox ID="TxtMaxDias" runat="server" MaxLength="9" 
                                                MinValue="0" Width="50px"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td><td><asp:RequiredFieldValidator ID="RfvMaxDias" runat="server" 
                                                ControlToValidate="TxtMaxDias" Display="Dynamic" ErrorMessage="*Requerido" 
                                                ForeColor="Red" ValidationGroup="AgregaCuenta"></asp:RequiredFieldValidator></td><td><table><tr id="tr2"><td><asp:ImageButton ID="BtnAgregarCuenta" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                                OnClick="imgAgregarCuenta_Click" ToolTip="Agregar" ValidationGroup="AgregarCuenta" /></td><td>&nbsp;</td><td>&nbsp;</td></tr></table></td></tr></table><table><tr><td></td><td><telerik:RadGrid ID="RgCredito" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgCredito_ItemCommand"
                                                OnNeedDataSource="rgCredito_NeedDataSource" OnPageIndexChanged="rgCredito_PageIndexChanged"><MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None"
                                                    TableLayout="Auto"><Columns><telerik:GridBoundColumn HeaderText="Id_Tu" UniqueName="Id_Tu" DataField="Id_Tu" Display="False"></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Tipo usuario" UniqueName="Tu_Descripcion" DataField="Tu_Descripcion"><HeaderStyle Width="250px" /></telerik:GridBoundColumn><telerik:GridBoundColumn HeaderText="Días" UniqueName="MaxDias" DataField="MaxDias"><HeaderStyle Width="50px" /><ItemStyle HorizontalAlign="Right" /></telerik:GridBoundColumn><telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar"><ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" /></telerik:GridButtonColumn></Columns><HeaderStyle HorizontalAlign="Center" /></MasterTableView><PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" /></telerik:RadGrid></td></tr></table></div></telerik:RadPageView>
                        
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenRebind" runat="server" />
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind() { }

                function combo_SelectedChanged() {
                    var row = document.getElementById("trAlertaDias");
                    var combo = $find("<%= cmbAlTipo.ClientID %>");
                    var rfv = document.getElementById("<%= RequiredFieldValidator7.ClientID %>");

                    var val = combo.get_value();

                    if (val == "SUS" || val == "LIM") {
                        row.style.display = 'none';
                        ValidatorEnable(rfv, false);
                    }
                    else {
                        row.style.display = '';
                        ValidatorEnable(rfv, true);
                    }
                }

                var D1;
                var D2;
                var D3;
                var D4;
                var D5;
                var D6;
                var validar;
                function tab_selected() {
                    validar = true;
                }
                function D1_Load()
                { D1 = $find("<%= txtDias1.ClientID %>"); }
                function D2_Load()
                { D2 = $find("<%= txtDias2.ClientID %>"); }
                function D3_Load()
                { D3 = $find("<%= txtDias3.ClientID %>"); }
                function D4_Load()
                { D4 = $find("<%= txtDias4.ClientID %>"); }
                function D5_Load()
                { D5 = $find("<%= txtDias5.ClientID %>"); }
                function D6_Load()
                { D6 = $find("<%= txtDias6.ClientID %>"); }
                function D_ValueChanged() {
                    validar = true;
                }

                var valida = true;
                function txt_rango1() {

                    if (D1.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }

                    if (D2.get_value() != "") {
                        if (D1.get_value() > D2.get_value()) {
                            D1.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D1._clientID);
                            return;
                        }
                    }

                    if (D3.get_value() != "" && D4.get_value() != "") {
                        if (D1.get_value() >= D3.get_value() && D1.get_value() <= D4.get_value()) {
                            D1.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D1._clientID);
                            return;
                        }
                    }
                    if (D5.get_value() != "" && D6.get_value() != "") {
                        if (D1.get_value() >= D5.get_value() && D1.get_value() <= D6.get_value()) {
                            D1.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D1._clientID);
                            return;
                        }
                    }
                }
                function txt_rango1_2() {
                    if (D2.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }

                    if (D1.get_value() != "") {
                        if (D1.get_value() > D2.get_value()) {
                            D2.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D2._clientID);
                            return;
                        }
                    }
                    else {
                        AlertaFocus("No ha capturado el inicio del rango", D1._clientID);
                        return;
                    }

                    if (D3.get_value() != "" && D4.get_value() != "") {
                        if (D2.get_value() >= D3.get_value() && D2.get_value() <= D4.get_value()) {
                            D2.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D2._clientID);
                            return;
                        }
                    }
                    if (D5.get_value() != "" && D6.get_value() != "") {
                        if (D2.get_value() >= D5.get_value() && D2.get_value() <= D6.get_value()) {
                            D2.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D2._clientID);
                            return;
                        }
                    }
                }
                function txt_rango2() {
                    if (D3.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }

                    if (D4.get_value() != "") {
                        if (D3.get_value() > D4.get_value()) {
                            D3.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D3._clientID);
                            return;
                        }
                    }
                    if (D1.get_value() != "" && D2.get_value() != "") {
                        if (D3.get_value() >= D1.get_value() && D3.get_value() <= D2.get_value()) {
                            D3.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D3._clientID);
                            return;
                        }
                    }
                    if (D5.get_value() != "" && D6.get_value() != "") {
                        if (D3.get_value() >= D5.get_value() && D3.get_value() <= D6.get_value()) {
                            D3.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D3._clientID);
                            return;
                        }
                    }

                }
                function txt_rango2_2() {
                    if (D4.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }

                    if (D3.get_value() != "") {
                        if (D3.get_value() > D4.get_value()) {
                            D4.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D4._clientID);
                            return;
                        }
                    }
                    else {
                        AlertaFocus("No ha capturado el inicio del rango", D3._clientID);
                        return;
                    }
                    if (D1.get_value() != "" && D2.get_value() != "") {
                        if (D4.get_value() >= D1.get_value() && D4.get_value() <= D2.get_value()) {
                            D4.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D4._clientID);
                            return;
                        }
                    }
                    if (D5.get_value() != "" && D6.get_value() != "") {
                        if (D4.get_value() >= D5.get_value() && D4.get_value() <= D6.get_value()) {
                            D4.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D4._clientID);
                            return;
                        }
                    }

                }
                function txt_rango3() {

                    if (D5.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }
                    if (D6.get_value() != "") {
                        if (D5.get_value() > D6.get_value()) {
                            D5.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D5._clientID);
                            return;
                        }
                    }
                    if (D1.get_value() != "" && D2.get_value() != "") {
                        if (D5.get_value() >= D1.get_value() && D5.get_value() <= D2.get_value()) {
                            D5.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D5._clientID);
                            return;
                        }
                    }
                    if (D3.get_value() != "" && D4.get_value() != "") {
                        if (D5.get_value() >= D3.get_value() && D5.get_value() <= D4.get_value()) {
                            D5.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D5._clientID);
                            return;
                        }
                    }
                }
                function txt_rango3_2() {

                    if (D6.get_value() == "" || !valida) {
                        valida = true;
                        return;
                    }
                    if (D5.get_value() != "") {
                        if (D5.get_value() > D6.get_value()) {
                            D6.set_value('');
                            valida = false;
                            AlertaFocus("El rango no es valido", D6._clientID);
                            return;
                        }
                    }
                    else {
                        AlertaFocus("No ha capturado el inicio del rango", D5._clientID);
                    }
                    if (D1.get_value() != "" && D2.get_value() != "") {
                        if (D6.get_value() >= D1.get_value() && D6.get_value() <= D2.get_value()) {
                            D6.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D6._clientID);
                            return;
                        }
                    }
                    if (D3.get_value() != "" && D4.get_value() != "") {
                        if (D6.get_value() >= D3.get_value() && D6.get_value() <= D4.get_value()) {
                            D6.set_value('');
                            valida = false;
                            AlertaFocus("Día ya contenido en otro rango", D6._clientID);
                            return;
                        }
                    }

                }
            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>

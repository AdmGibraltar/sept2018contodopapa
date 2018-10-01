<%@ Page Title="Criterios Venta Instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapCriterio_GVI.aspx.cs" Inherits="SIANWEB.CapCriterio_GVI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtNumeroCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtNombreCliente.ClientID %>'));
                //  LimpiarComboSelectIndex0($find('<%= cmbRSC.ClientID %>'));
                //  LimpiarComboSelectIndex0($find('<%= cmbFrecuencia.ClientID %>'));
                //  LimpiarComboSelectIndex0($find('<%= cmbTipoVisita.ClientID %>'));

                //  LimpiarCheckBox(document.getElementById('< %= chkActivo.ClientID % >'), true);

            }
             //   
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                //debugger;
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
                        //  LimpiarControles();
                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtNumeroCliente.ClientID %>');
                        txtId.enable();
                        txtId.focus();
                        continuarAccion = true;
                        break;
                    case 'undo':
                        continuarAccion = true;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            ///-------------------------------------------------------------------------------------
            //      Muestra el div con los controles de hora de inicio y fin
            ///-------------------------------------------------------------------------------------
            function showMe(it, box) {
                try {
                    var vis = (box.checked) ? "none" : "block";
                    element = document.getElementById("<%= divhoras.ClientID %>");
                    element.style.display = vis;
                    }
                catch (e) {
                    alert(e.toString());
                }
            }
            ///-------------------------------------------------------------------------------------
            //      Maneja las validaciones de las horas
            ///-------------------------------------------------------------------------------------
            var RadTimePicker1;
            var RadTimePicker2;

            function validate(sender, args) {
                var Date1 = new Date(tpHoraInicio.get_selectedDate());
                var Date2 = new Date(tpHoraFin.get_selectedDate());
                args.IsValid = true;
                if ((Date2 - Date1) < 0) {
                    alert("La hora de terminacion debe ser mayor a la hora de inicio!");
                    args.IsValid = false;
                }
            }

            function onLoadRadTimePicker1(sender, args) {
                RadTimePicker1 = sender;
            }

            function onLoadRadTimePicker2(sender, args) {
                RadTimePicker2 = sender;
            }
            ///-------------------------------------------------------------------------------------
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                                       
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    Enabled="false" ImageUrl="~/Imagenes/blank.png" Visible="false" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="1000px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table >
                        <tr>
                            <td>&nbsp;
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente" />
                            </td>
                            <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>                                    
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>                           
                        </tr>    
                        <tr>
                             <td>&nbsp;
                                <asp:Label ID="lblTipoVisita" runat="server" Text="Tipo de Visita" />
                            </td>
                            <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>   
                                        <td>
                                            <telerik:RadComboBox runat="server" ID="cmbTipoVisita" AutoPostBack="false"  Size="large" Width="250px">
                                                
                                            </telerik:RadComboBox> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                                <asp:Label ID="LblTerr" runat="server" Text="Frecuencia" />
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>   
                                        <td >
                                            <telerik:RadComboBox runat="server" ID="cmbFrecuencia" AutoPostBack="false" Size="Medium" >
                                            </telerik:RadComboBox> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>    
                        <tr>
                             <td>&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="RSC" />
                            </td>
                            <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>   
                                        <td >
                                            <telerik:RadComboBox runat="server" ID="cmbRSC" AutoPostBack="false"  Size="large"
                                            EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                                             ChangeTextOnKeyBoardNavigation="true" Filter="Contains"
                                               MaxHeight="300px" EmptyMessage="-- Seleccionar --" Width="400px" >
                                            <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                NoMatches="No hay coincidencias" />
                                            </telerik:RadComboBox> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td>&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Fecha" />
                            </td>
                            <td>&nbsp;
                                <telerik:RadDatePicker ID="txtFechaini" runat="server" Width="155px">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            &nbsp;&nbsp;&nbsp;
                                <asp:CheckBox  OnClick="Javascript:showMe('divhoras',this);" runat="server" ID="chkTodoElDia" Checked="true" Text="Todo el dia" AutoPostBack="false" Enabled="false" ></asp:CheckBox>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td colspan="5">
                                <div id="divhoras" runat="server" style="display:none">
                                    <table>
                                        <tr>
                                            <td>&nbsp;
                                                <asp:Label ID="Label5" runat="server" Text="Hora Inicio" />
                                            </td>
                                            <td>&nbsp;
                                                <telerik:RadTimePicker RenderMode="Lightweight" ID="tpHoraInicio" Width="185px" runat="server"
                                                     StartTime="08:00:00"  EndTime="19:00:10"
                                                     ControlToValidate="tpHoraInicio"  
                                                 >
                                                 <DateInput ID="DateInput1" runat="server">
                                                    <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                                </DateInput>
                                                 </telerik:RadTimePicker>
                                            </td>
                                            <td>&nbsp;
                                                <asp:Label ID="Label6" runat="server" Text="Hora Fin" />
                                            </td>
                                            <td>&nbsp;
                                                <telerik:RadTimePicker RenderMode="Lightweight" ID="tpHoraFin" Width="185px" runat="server"
                                                    StartTime="08:00:00"  EndTime="19:00:10"
                                                    ControlToValidate="tpHoraFin" 
                                                 >
                                                 <DateInput ID="DateInput2" runat="server">
                                                    <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                                </DateInput>
                                                 </telerik:RadTimePicker>
                                                 <asp:CustomValidator ID="CustomValidator1" EnableClientScript="true" runat="server"
                                                    ControlToValidate="tpHoraFin" ClientValidationFunction="validate">
                                                </asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td valign=top>&nbsp;
                                <asp:Label ID="Label4" runat="server" Text="PreRequisitos" />
                            </td>
                            <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                        <asp:CheckBoxList runat="server" ID="chklstPreRequisitos" AutoPostBack="false" 
                                            RepeatColumns="3" CellSpacing="2" CellPadding="2" Width="600px"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
             <tr>   
               <td>&nbsp;</td>
            </tr>
            <tr>
            <td>
             <table border="0">
                <tr>
                    <td>
                        <telerik:RadGrid ID="rgCriterios" runat="server" AutoGenerateColumns="false" GridLines="None"
                            PageSize="25" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnItemCommand="rgCriterios_ItemCommand" OnNeedDataSource="rgCriterios_NeedDataSource"
                            OnPageIndexChanged="rgCriterios_PageIndexChanged">
                            <MasterTableView DataKeyNames="Id_Cliente" DataMember="listCriterioCita">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Id_CriterioCita" UniqueName="Id_CriterioCita" DataField="Id_CriterioCita"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Id" UniqueName="Id_Cliente" DataField="Id_Cliente"
                                        Display="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="Cliente" DataField="Cliente">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Id_Frecuencia" UniqueName="Id_Frecuencia" DataField="Id_Frecuencia"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Frecuencia" UniqueName="Frecuencia" DataField="Frecuencia">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Id_TipoVisita" UniqueName="Id_TipoVisita" DataField="Id_TipoVisita"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TipoVisita" HeaderText="Tipo Visita" UniqueName="Tipo_Visita">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Id_RSC" UniqueName="Id_RSC" DataField="Id_RSC"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RSC" HeaderText="RSC/ASESOR/RIK" UniqueName="RSC">
                                        <HeaderStyle Width="250px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FechaInicial" HeaderText="Fecha Inicial" UniqueName="FechaInicial">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" Display="false"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn HeaderText="Asignar Fecha" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/calendar_32x32.png"
                                                CssClass="edit" ToolTip="Asignar Fecha" CommandName="Calendarizar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
    </div>
</asp:Content>

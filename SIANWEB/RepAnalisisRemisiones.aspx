<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" 
AutoEventWireup="true" CodeBehind="RepAnalisisRemisiones.aspx.cs" Inherits="SIANWEB.RepAnalisisRemisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function refreshGrid() {

            }

            //Validaciones especiales
            function ValidacionesEspeciales(sender, args) {
                //debugger;

                var AA = true;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= dpFechaini.ClientID %>');
                var datePickerFechaFin = $find('<%= dpFechafin.ClientID %>');

                if (datePickerFechaInicio != null && datePickerFechaFin != null) {

                    //realizar validaciones
                    var fechaInicio = null;
                    var fechaFin = null;

                    fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                    fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                    //validar rango correcto de fechas.
                    if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                        var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                        var alerta = radalert(mensage, 330, 150, tituloMensajes);

                        alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                        AA = false
                    }
                }
                args.set_cancel(!AA);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RblTipoCd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
       
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
        OnClientButtonClicking="ValidacionesEspeciales">
        <Items>
           <%-- <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />--%>
            <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                             <td>
                                &nbsp;
                            </td>
                            <td width="110">
                                <asp:RadioButtonList runat= "Server" ID="RblTipoRep" RepeatDirection="Horizontal" OnSelectedIndexChanged="RblTipoRep_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem runat="Server" Value ="1" Text ="Actual" Selected="True" ></asp:ListItem>
                                <asp:ListItem runat="Server" Value ="2" Text ="Cierre"  ></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td colspan= "3">
                                 &nbsp;
                            </td>

      
                           
                        </tr>
                   
                        
                    
  
 
      <tr runat="server" id="FechaIni" visible="false">
                            <td> &nbsp;
                            </td>
                            <td width="110">
                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px" Culture="es-MX"
                                    Enabled="False">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                             <td> &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="FechaFin" visible="false">
                             <td> &nbsp;
                            </td>
                            <td width="110">
                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final "></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px" Culture="es-MX"
                                    Enabled="False">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                             <td> &nbsp;
                            </td>
                        </tr>
                    

                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

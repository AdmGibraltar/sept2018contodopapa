<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage02.master" CodeBehind="Ventana_Calendario.aspx.cs" Inherits="SIANWEB.Ventana_Calendario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div  style="font-family: verdana; font-size: 8pt">
            Por favor, seleccione una fecha de corte para cada mes:
        <br/>
        <br />

        <table cellpadding="0" cellspacing="0" style="font-family: verdana; font-size: 8pt">
            <tr>
                <td>Enero: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calEnero" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="calEnero"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
            </tr>

           <tr>
                <td>Febrero: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calFebrero" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="calFebrero"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                       <tr>
                <td>Marzo: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calMarzo" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="calMarzo"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

            <tr>
                <td>Abril: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calAbril" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="calAbril"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

             <tr>
                <td>Mayo: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calMayo" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="calMayo"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                       <tr>
                <td>Junio: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calJunio" runat="server" Width="100px" 
                       Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="calJunio"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                       <tr>
                <td>Julio: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calJulio" runat="server" Width="100px" 
                      Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="calJulio"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                       <tr>
                <td>Agosto: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calAgosto" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="calAgosto"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                       <tr>
                <td>Septiembre: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calSeptiembre" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="calSeptiembre"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

             <tr>
                <td>Octubre: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calOctubre" runat="server" Width="100px" 
                       Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="calOctubre"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

              <tr>
                <td>Noviembre: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calNoviembre" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="calNoviembre"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>

                          <tr>
                <td>Diciembre: </td>              
                <td>
                
               <telerik:RadDatePicker ID="calDiciembre" runat="server" Width="100px" 
                      AutoPostBack="False" Culture="es-MX">
                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x" ShowRowHeaders="false">
                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                        TodayButtonCaption="Hoy" />
                </Calendar>
                <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
              </telerik:RadDatePicker>
                                                        
             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="calDiciembre"
                ErrorMessage="*" ></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>


            

           

        </table>
    </div>


    <br />
    <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
        Text="Aceptar" />


         <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
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

                function ValidacionFechas() {
                    alert("Las fechas no se encuentran en el orden correcto");
                }

                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind(param) {
                    //debugger;
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.ClienteSeleccionado(param);
                }
            </script>
        </telerik:RadCodeBlock>

</asp:Content>
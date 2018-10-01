

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pro_CN_Vinculacion.aspx.cs" Inherits="SIANWEB.Pro_CN_Vinculacion" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

         <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False" onajaxrequest="RAM1_AjaxRequest">
          <AjaxSettings>
               <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>


               <telerik:AjaxSetting AjaxControlID="btnAceptar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="txtClienteSIAN">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

        </AjaxSettings>
      </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <h3 style="font-family: Verdana;">Solicitud de <asp:Label ID="lblTitulo" runat="server" Text="Vinculación" /></h3>

              <div style="text-align:right">
                <asp:Button ID="btnAceptar" runat="server" text="Aceptar" 
                onclick="btnAceptar_Click"  /> &nbsp; 
                <asp:Button ID="btnCancelar" runat="server" text="Cancelar" CausesValidation="false" onclick="btnCancelar_Click"  />
                &nbsp; &nbsp;
             </div>
    
    <div style="font-family: Verdana; font-size: 8pt; height:599px; overflow:auto" id="divPrincipal" runat="server">

         <h4> Datos Generales </h4>

         <table runat="server" id="tabla1"> 

            <tr style="font-weight:bold">
                <td><asp:Label ID="Label25" runat="server" Text="Nombre" /></td>
                <td><asp:Label ID="txtNombreEstructura" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label102" runat="server" Text="CD" /></td>
                <td><asp:Label ID="txtSucursalNombre" runat="server" Text="" /></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Fecha" /></td>
                <td><asp:Label ID="txtFechas" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Usuario" /></td>
                <td><asp:Label ID="txtUsuario" runat="server" Text="" /></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label3" runat="server" Text="ACYS" /></td>
                <td><asp:Label ID="txtACYS" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td></td>
            </tr>
            
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Num. Cliente" /></td>
                <td>
                    <telerik:radnumerictextbox id="txtClienteSIAN" runat="server" enabled="True" maxlength="9" AutoPostBack="true"
                            minvalue="1" width="70px" OnTextChanged="txtClienteSIAN_TextChanged">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" /> 
                            <ClientEvents OnKeyPress="handleClickEvent" /> 
                            <EnabledStyle HorizontalAlign="Right" />
                    </telerik:radnumerictextbox>

                    <asp:RequiredFieldValidator ID="valCliente" runat="server" ErrorMessage="*(Requerido)" ControlToValidate="txtClienteSIAN"></asp:RequiredFieldValidator>
                        
                            <asp:Label ID="Label16" runat="server" Text="Terr." />

                            <telerik:RadComboBox ID="cmbTerritorio" runat="server"  
                                DataTextField="Ter_Nombre" DataValueField="Id_Ter"  EmptyMessage="Seleccione..."
                                EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                MaxHeight="250px" Width="300px">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>'
                                                    Width="50px" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ter_Nombre") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                          </telerik:RadComboBox>

                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*(Requerido)" ControlToValidate="cmbTerritorio"></asp:RequiredFieldValidator>

                   </td>
            </tr>

           <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Razón Social" /></td>
                <td><asp:TextBox ID="txtRazonSocial" runat="server" Text="" Width="400px"  ReadOnly="true"/></td>
            </tr>
         </table>

         <br />

        <h4> Datos fiscales SIAN CENTRAL </h4>
        <table runat="server" id="tabla2" >
        <tr>
            <td><asp:Label ID="Label21" runat="server" Text="Razónes sociales activas" /></td>
            <td><asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" 
                 ToolTip="Buscar" ValidationGroup="buscar" Visible="True" OnClick="ImgBuscarDireccionEntrega_Click"/>
             </td>
            <td></td>
        </tr>
        
        </table>


         <table runat="server" id="tabla3">
            <tr>
                <td><asp:Label ID="Label6" runat="server" Text="Calle" /></td>
                <td><asp:TextBox ID="txtCalle" runat="server" Text="" Width="200px" ReadOnly="true"/></td>
                <td><asp:Label ID="Label8" runat="server" Text="Número interior" /></td>
                <td><asp:TextBox ID="txtNumInterior" runat="server" Text="" ReadOnly="true"/></td>
                <td><asp:Label ID="Label9" runat="server" Text="Número Exterior" /></td>
                <td><asp:TextBox ID="txtNumExterior" runat="server" Text="" ReadOnly="true"/></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Colonia" /></td>
                <td><asp:TextBox ID="txtColonia" runat="server" Text="" Width="200px" ReadOnly="true"/></td>
               <td><asp:Label ID="Label10" runat="server" Text="Municipio" /></td>
                <td><asp:TextBox ID="txtMunicipio" runat="server" Text="" Width="200px" ReadOnly="true" /></td>

            </tr>

            <tr>
                <td><asp:Label ID="Label11" runat="server" Text="CP" /></td>
                <td><asp:TextBox ID="txtCP" runat="server" Text="" ReadOnly="true"/></td>
               <td><asp:Label ID="Label12" runat="server" Text="Estado" /></td>
                <td><asp:TextBox ID="txtEstado" runat="server" Text="" ReadOnly="true"/></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label13" runat="server" Text="Telefonos" /></td>
                <td><asp:TextBox ID="txtTelefonos" runat="server" Text="" ReadOnly="true"/></td>
               <td><asp:Label ID="Label14" runat="server" Text="FAX" /></td>
                <td><asp:TextBox ID="txtFAX" runat="server" Text="" ReadOnly="true"/></td>
                                <td><asp:Label ID="Label15" runat="server" Text="RFC" /></td>
                <td><asp:TextBox ID="txtRFC" runat="server" Text="" ReadOnly="true"/></td>
            </tr>


         </table>

         <h4>&nbsp;</h4>
         <table runat="server" id="tabla4">
            <tr style="color:White; background-color:#4b6896">
                 <td><asp:Label ID="Label17" runat="server" Text="Remisión Cta Nac" /></td>
                 <td><asp:Label ID="Label19" runat="server" Text="Asesor del Cliente" /></td>
            </tr>
            <tr>
                <td>
                   <telerik:RadComboBox ID="cmbRemision_Cta_Nac" runat="server"  
                                DataTextField="Nombre" DataValueField="Id"  EmptyMessage="Seleccione..."
                                EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                MarkFirstMatch="true" ReadOnly="False" 
                                MaxHeight="250px" Width="300px">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                <asp:Label ID="Label18" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                    Width="50px" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                          </telerik:RadComboBox>
                </td>
                 <td>
                          <telerik:RadComboBox ID="cmbAsesorId" runat="server"  
                                DataTextField="Nombre" DataValueField="Id"  EmptyMessage="Seleccione..."
                                EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                MarkFirstMatch="true" ReadOnly="False"
                                MaxHeight="250px" Width="300px">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                    Width="50px" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                          </telerik:RadComboBox>
                </td>
            </tr>
         </table>


            <table runat="server" id="tabla5">
             <tr style="color:White; background-color:#4b6896">
                <td><asp:Label ID="Label20" runat="server" Text="Comentarios" /></td>
             </tr>
             <tr>
                 <td><asp:TextBox ID="txtComentarios" runat="server" Text="" TextMode="MultiLine" Width="600px" Height="50px" /></td>
             </tr>
           </table>

      
    </div>

<telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    function AbrirBuscarDireccionEntrega(idMatriz) {
        var oWnd = radopen("Ventana_Buscar.aspx?CN_IdMatriz=" + idMatriz, "AbrirVentana_BuscarPrecio");
        oWnd.setSize(800, 600);
        oWnd.center();
    }

    function ClienteSeleccionado(param) {
        var ajaxManager = $find("<%= RAM1.ClientID %>");
        ajaxManager.ajaxRequest(param);
    }

    function CloseWindow() {
        GetRadWindow().Close();
        GetRadWindow().BrowserWindow.refreshGrid(null);
    }

    function CloseAlert(mensaje) {
        //                var cerrarWindow = radalert(mensaje, 330, 150);
        //                cerrarWindow.add_close(
        //                    function () {

        alert(mensaje);
        CloseWindow();
        //                    });
    }


    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
        return oWindow;
    }


    </script>
</telerik:radcodeblock>

</asp:Content>
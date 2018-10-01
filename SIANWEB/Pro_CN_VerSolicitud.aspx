<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudesItem.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.SolicitudesItem" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <h3 style="font-family: Verdana;">Solicitud</h3>


    <div style="font-family: Verdana; font-size: 8pt;">
         <h4> Datos Generales </h4>

         <table>
            <tr>
                <td><asp:Label ID="Label102" runat="server" Text="CD" /></td>
                <td><asp:Label ID="txtSucursalNombre" runat="server" Text="" /></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Fecha" /></td>
                <td><asp:Label ID="txtFecha" runat="server" Text="" /></td>
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
                <td><asp:Label ID="txtClienteSIAN" runat="server" Text="" /></td>
                <td><asp:Label ID="Label16" runat="server" Text="Terr." /></td>
                <td><asp:Label ID="txtTerritorio" runat="server" Text="" /></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Razón Social" /></td>
                <td><asp:Label ID="txtRazonSocial" runat="server" Text="" /></td>
            </tr>
         </table>

         <br />

         <h4> Datos fiscales del SIANWEB del CDIK </h4>
         <table>
            <tr>
                <td><asp:Label ID="Label6" runat="server" Text="Calle" /></td>
                <td><asp:Label ID="txtCalle" runat="server" Text="" /></td>
                <td><asp:Label ID="Label8" runat="server" Text="Número interior" /></td>
                <td><asp:Label ID="txtNumInterior" runat="server" Text="" /></td>
                <td><asp:Label ID="Label9" runat="server" Text="Número Exterior" /></td>
                <td><asp:Label ID="txtNumExterior" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Colonia" /></td>
                <td><asp:Label ID="txtColonia" runat="server" Text="" /></td>
               <td><asp:Label ID="Label10" runat="server" Text="Municipio" /></td>
                <td><asp:Label ID="txtMunicipio" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label11" runat="server" Text="CP" /></td>
                <td><asp:Label ID="txtCP" runat="server" Text="" /></td>
               <td><asp:Label ID="Label12" runat="server" Text="Estado" /></td>
                <td><asp:Label ID="txtEstado" runat="server" Text="" /></td>
            </tr>

           <tr>
                <td><asp:Label ID="Label13" runat="server" Text="Telefonos" /></td>
                <td><asp:Label ID="txtTelefonos" runat="server" Text="" /></td>
               <td><asp:Label ID="Label14" runat="server" Text="FAX" /></td>
                <td><asp:Label ID="txtFAX" runat="server" Text="" /></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label15" runat="server" Text="RFC" /></td>
                <td><asp:Label ID="txtRFC" runat="server" Text="" /></td>
            </tr>
         </table>

         <h4>&nbsp;</h4>
         <table>
            <tr style="color:White; background-color:#4b6896">
                <td><asp:Label ID="Label17" runat="server" Text="Remisión Cta Nac" /></td>
                <td><asp:Label ID="Label18" runat="server" Text="Ligar a Convenio" /></td>
                 <td><asp:Label ID="Label19" runat="server" Text="Asesor del Cliente" /></td>
            </tr>
            <tr>
                <td>
                   <asp:Label ID="txtRemision_Cta_Nac_Nombre_Desc" runat="server" Text="" Width="200px" />
                </td>

                <td>
                     <asp:Label ID="txtConvenioPENombre" runat="server" Text="" Width="200px" />
                </td>

                 <td>
                     <asp:Label ID="txtAsesorNombre" runat="server" Text="" Width="200px" />
                </td>
            </tr>
         </table>

         <br />

            <table>
             <tr style="color:White; background-color:#4b6896">
                <td><asp:Label ID="Label20" runat="server" Text="Comentarios" /></td>
             </tr>
             <tr>
                 <td><asp:Label ID="txtComentarios" runat="server"  Width="600px" Height="50px" /></td>
             </tr>
           </table>

        <br />

         <table>
             <tr style="color:White; background-color:#4b6896">
                <td><asp:Label ID="Label21" runat="server" Text="Observaciones Administrador" /></td>
             </tr>
             <tr>
                 <td><asp:TextBox ID="txtComentariosAdministrador" runat="server" TextMode="Multiline"  Width="600px" Height="50px" /></td>
             </tr>
           </table>

    </div>

<br />

<asp:Button ID="btnAceptar" runat="server" text="Autorizar" 
        onclick="btnAceptar_Click" /> &nbsp; <asp:Button ID="btnRechazar" 
        runat="server" text="Rechazar" onclick="btnRechazar_Click" />

 
 <telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
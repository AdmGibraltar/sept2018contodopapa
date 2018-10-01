<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="Pro_CN_Solicitudes.aspx.cs" Inherits="SIANWEB.Pro_CN_Solicitudes" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"> 
   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" 
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>

                </UpdatedControls>

            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

       
    <h4>Mis Solicitudes</h4>

    <table style="font-family:Verdana;font-size:8pt">
        <tr>
            <td><asp:Label ID="lbl1" Text="Sucursal: " runat="server"></asp:Label></td>
            <td><asp:Label ID="lblSucursal" Text="" runat="server"></asp:Label></td>
        </tr>
        <tr>
             <td><asp:Label ID="Label1" Text="Usuario: " runat="server"></asp:Label></td>
            <td><asp:Label ID="lblUsuario" Text="" runat="server"></asp:Label></td>
        </tr>
    </table>


        <br />

  <div style="overflow:auto; height:80%">
     <asp:GridView ID="dgSolicitudes" runat="server" AutoGenerateColumns="false" >
        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="Id" Visible="True" />
            
            <asp:BoundField ReadOnly="True" HeaderText="#Cliente" DataField="ClienteSIAN" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="RazonSocial" HeaderStyle-Width="150"/>
            
            <asp:BoundField ReadOnly="True" HeaderText="Terr" DataField="TerrNombre" HeaderStyle-Width="30"/>
            <asp:BoundField ReadOnly="True" HeaderText="Estructura" DataField="EstructuraNombre" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="ACYS" DataField="ACYSNombre" HeaderStyle-Width="100"/>

           
            <%--<asp:BoundField ReadOnly="True" HeaderText="Matriz" DataField="Matriz" HeaderStyle-Width="150" />--%>
<%--            <asp:BoundField ReadOnly="True" HeaderText="Estructura" DataField="Estructura" HeaderStyle-Width="200"/>--%>
            <asp:BoundField ReadOnly="True" HeaderText="Solicitud" DataField="SolicitudEstatus" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Usuario" DataField="Usuario" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Fecha" DataField="Fecha" HeaderStyle-Width="100" DataFormatString="{0:d}"/>


           <asp:TemplateField HeaderText="Ver">
                    <ItemTemplate>
                         <img src="../Img/ic_edit.jpg" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "Id_Matriz") %> );" />
                    </ItemTemplate>
           </asp:TemplateField>
    
          <%--  <asp:BoundField ReadOnly="True" HeaderText="Estatus" DataField="EstatusNombre" HeaderStyle-Width="100"/>
--%>                
        </Columns>
    </asp:GridView>
</div>

       <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function AbrirPantallaVinculacion(Id, IdMatriz) {
                oWnd = radopen('Pro_CN_VerSolicitud.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz);
                oWnd.maximize();
            }


           
        </script>
      </telerik:radcodeblock>



</asp:Content>

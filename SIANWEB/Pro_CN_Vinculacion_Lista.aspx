<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="Pro_CN_Vinculacion_Lista.aspx.cs" Inherits="SIANWEB.Pro_CN_Vinculacion_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"> 
   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">

        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="dgClienteMatriz" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="cmbMatriz">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgClienteMatriz" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="dgClienteMatriz">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgClienteMatriz" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

    </telerik:RadAjaxManager>


    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" >
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
            </Items>
     </telerik:RadToolBar>


<div style="font-family:Verdana; width:80%">


<br />
    <table style="font-family: Verdana; font-size: 8pt; width:900px">
      <tr>
        <td><asp:Label ID="lblNombre" Text="Matriz: " runat="server"></asp:Label></td>
        <td>
           
           <telerik:RadComboBox ID="cmbMatriz" runat="server"  ChangeTextOnKeyBoardNavigation="true"
            DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..."
            EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
            MarkFirstMatch="true" 
                Width="370px" ReadOnly="True"
            MaxHeight="250px" OnSelectedIndexChanged="cmbMatriz_SelectedIndexChanged" AutoPostBack="true">
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 25px; text-align: center; vertical-align: top">
                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                Width="50px" />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
                        </td>

                    </tr>
                    </table>
	            </ItemTemplate>
            </telerik:RadComboBox>

            
        </td>
                               <td>
                            &nbsp;
                        </td>
                        <td style="text-align:right">
                            <a href="#" onclick=" AbrirPantallaSolicitudes()">Mis Solicitudes</a> 
                        </td>
      </tr>
    </table>
    <br />
   

    <div style="overflow:auto; width:900px;height:430px">
     <asp:GridView ID="dgClienteMatriz" runat="server" AutoGenerateColumns="false" OnRowCommand="dgClienteMatriz_RowCommand">
        <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="Id" />
            <asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="NombreNodo"   HeaderStyle-Width="100" />
            <asp:BoundField ReadOnly="True" HeaderText="Matriz" DataField="NombreMatriz"  HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="ACYS" DataField="AcysNombre" HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Estatus" DataField="SolicitudEstatus"  HeaderStyle-Width="100"/>
            <asp:BoundField ReadOnly="True" HeaderText="Cte Vinculado" DataField="ClienteVinculado"  HeaderStyle-Width="100"/>
                      
<asp:TemplateField HeaderText="Ver ACYS">
                    <ItemTemplate>
                         <img src="Img/find16.png" id="imgEditar" style="cursor:pointer" onclick="AbrirPantallaACYS(<%# DataBinder.Eval(Container.DataItem, "id_Acys") %>, <%# DataBinder.Eval(Container.DataItem, "Id_Matriz") %> );" />
                    </ItemTemplate>
           </asp:TemplateField>

            <asp:TemplateField HeaderText="Vincular">
                    <ItemTemplate>
                         <img  <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==0? " src='Img/Ic_2.jpg'  ": "  style='display:none' "  %> id="imgPermisos" style="cursor:pointer" onclick="AbrirPantallaVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "Id_Matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "NombreNodo") %>' );" />
                     </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Desvincular">
                    <ItemTemplate>
                         <img <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==2   ? "src='Img/x.gif' " : "style='display:none'"  %> id="imgPermisos" style="cursor:pointer" onclick="AbrirPantallaDesVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "Id_Matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "NombreNodo") %>' );" />
                     </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ver Solicitud">
                    <ItemTemplate>
                         <img <%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1 || Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5  ? "src='img/ic_1.gif' ": "style='display:none'"  %>  id="imgPermisos" style="cursor:pointer" onclick="AbrirPantallaVinculacion(<%# DataBinder.Eval(Container.DataItem, "Id") %>, <%# DataBinder.Eval(Container.DataItem, "Id_Matriz") %> , '<%# DataBinder.Eval(Container.DataItem, "NombreNodo") %>');" />
                     </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Cancelar Solicitud">
                    <ItemTemplate>
                        <asp:ImageButton CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Cancelar" OnClientClick="return confirmar();" runat="server" ID="btnCancelar" ImageUrl='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1 || Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? "img/quitar1.png": ""  %>' Visible='<%# Int32.Parse(DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==1|| Int32.Parse( DataBinder.Eval(Container.DataItem, "IdEstatus").ToString())==5 ? true: false  %>' />
                   </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>

</div>

<%--    <asp:Button ID="btnAceptar" runat="server" text="test" 
        onclick="btnAceptar_Click"  />--%>
    
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirPantallaACYS(Id, IdMatriz) {
                oWnd = radopen('Cat_CN_ClienteMatriz_ACYS.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz);
                oWnd.maximize();
            }

            function AbrirPantallaVinculacion(Id, IdMatriz, Nombre) {
                oWnd = radopen('Pro_CN_Vinculacion.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz + '&Nombre=' + Nombre);
                oWnd.maximize();
            }


            function AbrirPantallaDesVinculacion(Id, IdMatriz) {
                oWnd = radopen('Pro_CN_Vinculacion.aspx?Id=' + Id + '&IdMatriz=' + IdMatriz + '&DesVinc=1');
                oWnd.maximize();
            }

            function AbrirPantallaSolicitudes() {
                oWnd = radopen('Pro_CN_Solicitudes.aspx');
                oWnd.maximize();
            }

            function confirmar() {
                return confirm('Está seguro que desa cancelar la solicitud');
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
            

        </script>
      </telerik:radcodeblock>



</asp:Content>
<%@ Page Title="Facturas canceladas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" 
AutoEventWireup="true" CodeBehind="RepFacturasCanceladasSAT.aspx.cs" Inherits="SIANWEB.RepFacturasCanceladasSAT" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
 <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function refreshGrid() {
            }              
          
        </script>
    </telerik:radcodeblock>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
        
    </telerik:radajaxloadingpanel>
         <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>                                                                        
        
        
                       
        </AjaxSettings>
    </telerik:radajaxmanager>

    <div>
        <telerik:radtoolbar id="RadToolBar1" runat="server" width="100%" dir="rtl" 
            onbuttonclick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir"
                CssClass="print" ValidationGroup="imprimir" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="imprimir" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:radtoolbar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server" width="150px"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" onselectedindexchanged="cmbCentrosDist_SelectedIndexChanged"
                        width="150px" autopostback="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <div id="filtros" runat="server">            
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                <td width="15px" ></td>
                    <td>
                       <asp:Label ID="Label4" runat="server" Text="Año"  width="70px" ></asp:Label>
                    </td>
                    <td>                                                               
                        <telerik:radcombobox id="cmbAnio" runat="server" width="120px" filter="Contains" changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                        highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id"  MaxHeight="250px"
                        >                           
                        </telerik:radcombobox >

                    </td>   
                    <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbAnio"
                         ErrorMessage="* Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="imprimir"></asp:RequiredFieldValidator>
                    </td>                                    
                </tr>
                  <tr> 
                  <td width="15px" ></td>                                     
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Més" width="70px"></asp:Label>
                    </td>
                    <td>                    
                        <telerik:radcombobox id="cmbMes" runat="server" width="120px" filter="Contains"
                            changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                            highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id"  MaxHeight="250px"
                             >                           
                            </telerik:radcombobox>
                    </td>
                    <td>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbMes"
                          ErrorMessage="* Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="imprimir"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <br />
                <tr> 
                <td width="15px" ></td>                                     
                    <td>
                        &nbsp;</td>
                     <td valign="top" width="120">  
                         &nbsp;</td>                                     
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>                              
                       
           
            &nbsp;<asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>

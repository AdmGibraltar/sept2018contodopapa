<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="Default.aspx.cs" Inherits="CANCELAWEBSERVICE._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <style type="text/css">
        .style1
        {
            width: 204px;
        }
        .style2
        {
            width: 204px;
            height: 30px;
        }
        .style8
        {
            width: 507px;
        }
        </style>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"> 
  </telerik:RadWindowManager> 
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">        
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>  
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
      </telerik:RadAjaxManager>  
    <h2>
    
    </h2>

     <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style8">
               
            </td>
            <td>
                <telerik:RadToolBar ID="RadToolBar1" Runat="server" SingleClick="None" OnButtonClick="RadToolBar1_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton runat="server" CommandName="Cancelar" Text="Ejecutar">
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </td>
        </tr>
        <tr>
            <td class="style2">
            RFC
            </td>
            <td colspan="2">
             <telerik:RadTextBox ID="RadRFC" Runat="server" LabelWidth="64px" 
                    Resize="None" Text="RFC" Width="160px">
                </telerik:RadTextBox>
                &nbsp;</td>
         </tr>
         <tr>
            <td class="style2">
            UUID
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="RadUUI" Runat="server" LabelWidth="64px" 
                    Resize="None" Text="UUID" Width="160px">
                </telerik:RadTextBox>
             </td>
         </tr>
        </table>
    
</asp:Content>

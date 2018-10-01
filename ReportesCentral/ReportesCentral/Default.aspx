<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ReportesCentral._Default" %>

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
        .style3
        {
            height: 30px;
        }
        .style4
        {
            width: 204px;
            height: 41px;
        }
        .style5
        {
            height: 41px;
        }
        .style6
        {
            width: 204px;
            height: 38px;
        }
        .style7
        {
            height: 38px;
        }
        .style8
        {
            width: 507px;
        }
        .style9
        {
            height: 30px;
            width: 507px;
        }
        .style10
        {
            height: 41px;
            width: 507px;
        }
        .style11
        {
            height: 38px;
            width: 507px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<telerik:RadWindowManager ID="RadWindowManager1" runat="server"> 
  </telerik:RadWindowManager> 
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server" AsyncPostBackTimeout="7000">        
    </telerik:RadScriptManager>   
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
<div id="divPrincipal" runat="server">   
       <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                <telerik:RadToolBar ID="RadToolBar1" Runat="server" SingleClick="None" OnButtonClick="RadToolBar1_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton runat="server" CommandName="Exportar" Text="Exportar">
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Reporte</td>
            <td class="style9">
                <telerik:RadComboBox ID="RadComboReporte" Runat="server" Width="290px">
                </telerik:RadComboBox>
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style4">
                Año</td>
            <td class="style10">
                <telerik:RadComboBox ID="RadComboAno" Runat="server">
                </telerik:RadComboBox>
            </td>
            <td class="style5">
            </td>
        </tr>
        <tr>
            <td class="style6">
                Mes</td>
            <td class="style11">
                <telerik:RadComboBox ID="RadComboMes" Runat="server">
                </telerik:RadComboBox>
            </td>
            <td class="style7">
            </td>
        </tr>
    </table>
   
    <br />
    <telerik:RadDockLayout ID="RadDockLayout1" Runat="server">
        <img src="Img/Inicio.png" style="height: 417px" />
    </telerik:RadDockLayout>
&nbsp;
    


    </div>
</asp:Content>

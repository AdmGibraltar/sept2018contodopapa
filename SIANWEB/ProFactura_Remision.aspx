<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="ProFactura_Remision.aspx.cs" Inherits="SIANWEB.ProFacturaRemision" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
     <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Aceptar">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton Text="Cancelar">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" onneeddatasource="RadGrid1_NeedDataSource">
<MasterTableView>
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridBoundColumn DataField="Remision" HeaderText="Remision" 
            UniqueName="column">
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha de" 
            UniqueName="column1">
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Num" HeaderText="Num. Cte." 
            UniqueName="column2">
            <HeaderStyle Width="50px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" 
            UniqueName="column3">
            <HeaderStyle Width="100px" />
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

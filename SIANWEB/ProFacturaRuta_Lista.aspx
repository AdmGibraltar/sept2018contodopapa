<%@ Page Title="Embarque de facturas por ruta" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProFacturaRuta_Lista.aspx.cs" Inherits="SIANWEB.ProFacturaRuta_Lista" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Abrir" CommandName="Abrir">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton Text="Eliminar" CommandName="Eliminar">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton Text="Modificar" CommandName="Modificar">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton Text="Imprimir" CommandName="Imprimir">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: verdana; font-size: 8pt">
                        <tr>
                            <td>
                            </td>
                            <td width="100">
                            </td>
                            <td>
                            </td>
                            <td width="100">
                            </td>
                            <td width="50">
                            </td>
                            <td width="150">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha incial
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                Fecha final
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Embarque
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEmbarqueini" runat="server" Width="90px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                Estatus
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="130px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgFactura" runat="server" GridLines="None" Width="582px" OnNeedDataSource="RadGrid1_NeedDataSource"
                        Height="200px" AutoGenerateColumns="False">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Usuario" HeaderText="Usuario" UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Embarque" HeaderText="Embarque" UniqueName="column2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dia" HeaderText="Día" UniqueName="column4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Chofer" HeaderText="Chofer" UniqueName="column5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Camion" HeaderText="Camion" UniqueName="column6">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>

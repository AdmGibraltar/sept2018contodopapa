<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="ProVentInst_PedidoNoCap.aspx.cs" Inherits="SIANWEB.ProVentInst_PedidoNoCap" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Text="Aceptar">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
    <table style="font-family: Verdana; font-size: 8pt">
        <tr>
            <td>
            </td>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                            PageViewID="RadPageViewDGenerales" Selected="True">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" BorderStyle="Solid"
                        BorderWidth="1px">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td width="70">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="70">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="70">
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                </td>
                                <td width="70">
                                </td>
                                <td width="200">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    Folio
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox23" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Acuerdo
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox24" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Cliente
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox20" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="4">
                                    <telerik:RadComboBox ID="RadComboBox2" runat="server" Width="250px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="5" rowspan="10">
                                    <table>
                                        <tr>
                                            <td>
                                                <b>Contacto</b>
                                            </td>
                                            <td width="175">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nombre
                                            </td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" runat="server" Width="175px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Puesto
                                            </td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox2" runat="server" Width="175px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Telefono
                                            </td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox3" runat="server" Width="175px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                E-Mail
                                            </td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox4" runat="server" Width="175px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Territorio
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox19" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="2">
                                    &nbsp;
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
                                    Representante
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox22" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="4">
                                    <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="250px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Via de captación
                                </td>
                                <td colspan="5">
                                    <telerik:RadTextBox onpaste="return false" ID="RadTextBox13" runat="server" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Semana
                                </td>
                                <td colspan="2">
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox17" runat="server" Width="40px">
                                    </telerik:RadNumericTextBox>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox18" runat="server" Width="50px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="RadGrid1_NeedDataSource">
                            <MasterTableView>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Prod" HeaderText="Prod." UniqueName="column">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Presen" HeaderText="Presen." UniqueName="column2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Unidad" HeaderText="Unidad" UniqueName="column3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Frec" HeaderText="Frec. cada (n) sem." UniqueName="column4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sem" HeaderText="Sem. ped. ant." UniqueName="column5">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ventaanterior" HeaderText="Venta anterior ultimos 3 meses"
                                        UniqueName="column6">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VentaCaptada" HeaderText="Venta captada" UniqueName="column7">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Precio" HeaderText="Precio" UniqueName="column8">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="column9">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td width="250">
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
                            <tr>
                                <td>
                                    Motivo
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBox3" runat="server" Width="250px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Subtotal
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox14" runat="server">
                                    </telerik:RadNumericTextBox>
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
                                    I.V.A.
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox15" runat="server">
                                    </telerik:RadNumericTextBox>
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
                                    Total
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox16" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>

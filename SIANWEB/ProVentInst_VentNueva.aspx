<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="ProVentInst_VentNueva.aspx.cs" Inherits="SIANWEB.Facturacion.Procesos.ProVentInst_VentNueva" %>
 
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
                                <td>
                                </td>
                                <td>
                                </td>
                                <td width="110">
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
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
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
                                    <telerik:RadComboBox ID="RadComboBox1" Runat="server" Width="200px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="5" rowspan="9">
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
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Representante
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox22" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="4">
                                    <telerik:RadComboBox ID="RadComboBox2" Runat="server" Width="200px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Semana
                                </td>
                                <td colspan="2">
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox17" runat="server" Width="40px">
                                    </telerik:RadNumericTextBox>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox18" runat="server" Width="50px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Fecha de factura
                                </td>
                                <td colspan="2">
                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                        Orden de
                                    </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox21" runat="server" Width="70px">
                                    </telerik:RadNumericTextBox>
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
                                OnNeedDataSource="RadGrid1_NeedDataSource" Width="800px" Height="200px">
                            <MasterTableView>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Prod" HeaderText="Prod." UniqueName="column">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Presen" HeaderText="Presen." UniqueName="column2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Unidad" HeaderText="Unidad" UniqueName="column3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ventaanterior" HeaderText="Venta anterior ultimos 3 meses"
                                            UniqueName="column4">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ventacaptada" HeaderText="Venta captada" UniqueName="column5">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Precio" HeaderText="Precio" UniqueName="column6">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="column7">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Docentrega" HeaderText="Doc. de entrega" UniqueName="column8">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <table width="800">
                            <tr>
                                <td rowspan="4">
                                    <table style="width:100%;">
                                        <tr>
                                            <td colspan="2">
                                            Consignado</td>
                                            <td>
                                            &nbsp;</td>
                                            <td width="100">
                                            &nbsp;</td>
                                            <td colspan="2" width="100">
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width="60">
                                            Calle</td>
                                            <td width="200">
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox7" Runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td width="60">
                                            No.</td>
                                            <td>
                                            &nbsp;</td>
                                            <td style="width: 0">
                                            C.P.</td>
                                            <td style="width: 50px">
                                                <telerik:RadNumericTextBox ID="RadNumericTextBox25" Runat="server" Width="65px">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Colonia</td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox8" Runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                            Municipio</td>
                                            <td colspan="3">
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox11" Runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Ciudad</td>
                                            <td>
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox9" Runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                            Estado</td>
                                            <td colspan="3">
                                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox10" Runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="10">
                                    &nbsp;</td>
                                <td width="50">
                                    &nbsp;</td>
                                <td width="125">
                                    &nbsp;</td>
                                <td width="40">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                        &nbsp;</td>
                                <td>
                                        Subtotal
                                    </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox14" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                        &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                        &nbsp;</td>
                                <td>
                                        I.V.A.
                                    </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox15" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                        &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                        &nbsp;</td>
                                <td>
                                        Total
                                    </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBox16" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                        &nbsp;</td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    Nota</td>
                                <td>
                                    <telerik:RadTextBox onpaste="return false" ID="RadTextBox12" runat="server" Width="520px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>

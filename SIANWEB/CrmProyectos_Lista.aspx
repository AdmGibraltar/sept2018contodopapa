<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CrmProyectos_Lista.aspx.cs" Inherits="SIANWEB.CrmProyectos_Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                UEN
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Area
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox4" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Segmento
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox2" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Solución
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox5" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Territorio
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox3" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Aplicación
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox6" runat="server">
                                </telerik:RadComboBox>
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
                                Estatus
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox7" runat="server">
                                </telerik:RadComboBox>
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
                                <asp:Button ID="Button1" runat="server" Text="Aplicar filtro" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="RadGrid1_NeedDataSource" Width="842px">
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Clave" HeaderText="Clave" UniqueName="column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IdCliente" HeaderText="No. Cliente" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Proyecto" HeaderText="Proyecto" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Potencial" HeaderText="Potencial" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="A" HeaderText="A" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="P" HeaderText="P" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="N" HeaderText="N" UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="C" HeaderText="C" UniqueName="column8">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="X" HeaderText="X" UniqueName="column9">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Avances" HeaderText="Avances" UniqueName="column10">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        </table>
                    <table>
                        <tr>
                        <td>
                        
                            &nbsp;</td>
                        <td align="center">
                        
                            &nbsp;</td>
                        <td align="center" colspan="4">
                        
                            &nbsp;</td>
                        <td align="center">
                        
                            &nbsp;</td>
                        <td align="center">
                        
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <td>
                        
                        </td>
                        <td align="center">
                        
                            TOTAL</td>
                        <td align="center" colspan="4">
                        
                            MONTO POR ETAPA</td>
                        <td align="center">
                        
                            X</td>
                        <td align="center">
                        
                            AVANCE&nbsp;</td>
                        </tr>
                        <tr>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        <td>
                        
                            &nbsp;</td>
                        </tr>
                        <tr>
                        <td width="300">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        <td width="70">
                        
                            &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

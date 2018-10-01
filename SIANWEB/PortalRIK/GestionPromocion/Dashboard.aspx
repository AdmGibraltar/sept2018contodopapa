<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SIANWEB.PortalRIK.Dashboard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <%--<meta http-equiv="X-UA-Compatible" content="IE=Edge" />--%>
    <%--<link href="<%= Page.ResolveUrl("~/css/general.css")%>" rel="stylesheet" type="text/css" />--%>
    <script src="<%= Page.ResolveUrl("~/js/swf.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
    <form runat="server">
        <!--RadScriptManager-->
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptGlobalization="True"
            EnableScriptLocalization="True" EnableTheming="true" AsyncPostBackTimeout="36000">
        </telerik:RadScriptManager>
        <div id="centrador">
            <!--Inicia header-->
            <!--Termina header-->
            <!--Inicia contenido-->
            <div class="contenido" runat="server" id="contenido">
                <table id="tblPrin" align="center">
                    <tr>
                        <td align="right">
                            <asp:Panel ID="pnlComercial" runat="server">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Núm. proyectos"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Monto proyectos"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label4" runat="server" Text="Avances mes"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label6" runat="server" Text="Cantidad cerrados"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label5" runat="server" Text="Monto cerrados"></asp:Label>
                                        </td>
                                        <td width="100">
                                            &nbsp;
                                        </td>
                                        <td width="100">
                                            <asp:Label ID="lblcd" runat="server" Text="CD:"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlCDS" runat="server" AutoPostBack="True" CssClass="inp1"
                                                OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px" align="center">
                                            <telerik:RadNumericTextBox ID="NumProyectos" runat="server" ReadOnly="true" size="5"
                                                Style="text-align: center" Width="80px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <telerik:RadNumericTextBox ID="MontoProyectos" runat="server" ReadOnly="true" size="5"
                                                Style="text-align: center" Width="80px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <telerik:RadNumericTextBox ID="AvanceMes" runat="server" ReadOnly="true" size="5"
                                                Style="text-align: center" Width="80px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <telerik:RadNumericTextBox ID="CantidadCerrados" runat="server" ReadOnly="true" size="5"
                                                Style="text-align: center" Width="80px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <telerik:RadNumericTextBox ID="MontoCerrados" runat="server" ReadOnly="true" size="5"
                                                Style="text-align: center" Width="80px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkMetas" runat="server" PostBackUrl="wfrmMetasTerritorio.aspx">Establecer metas y cuotas</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlGeneral" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            Vista de gráficas:&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddl" runat="server" AutoPostBack="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Importe de proyectos" Value="1" />
                                                    <telerik:RadComboBoxItem Text="Número de proyectos" Value="2" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlMeses" runat="server" AutoPostBack="True" Visible="False">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 81px">
                            <div id="Label1">
                            </div>
                            <div>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <%=GeneraGraficaDistribucion()%>
                                            </td>
                                            <td>
                                                <%=GeneraGraficaActividad()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <div runat="server" id="divImprimir" visible="false">
                                                    &nbsp;<asp:LinkButton ID="ibtnImprimir" runat="server" CssClass="btn_imprimir">IMPRIMIR</asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Termina contenido-->
            <!--Inicia footer-->
        
            <!--Termina footer-->
        </div>
    </form>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            
        });

        function printpage() {
            window.print();
        }  
    </script>
</asp:Content>

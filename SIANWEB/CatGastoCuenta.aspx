<%@ Page Title="Cuentas de Gastos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatGastoCuenta.aspx.cs" Inherits="SIANWEB.CatGastoCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcreedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
        <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:radtoolbar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="LblClave" runat="server" Text="Clave"></asp:Label>
                </td>
                <td>
                    <telerik:radnumerictextbox id="TxtClave" runat="server" width="70px" minvalue="1"
                        autopostback="true" ontextchanged="TxtClave_TextChanged">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        <ClientEvents OnKeyPress="handleClickEvent" />
                    </telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="LblDescripcion" runat="server" Text="Descripción"></asp:Label>
                </td>
                <td colspan="5">
                    <telerik:radtextbox id="TxtDescripcion" runat="server" width="200px"></telerik:radtextbox>
                    <asp:RequiredFieldValidator ID="RfvDescripcion" runat="server" ControlToValidate="TxtDescripcion"
                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblNumero" runat="server" Text="Numero"></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="TxtNumero" runat="server"></telerik:radtextbox>
                </td>
                <td>
                    <asp:Label ID="LblCC" runat="server" Text="C.C."></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="TxtCC" runat="server"></telerik:radtextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblSubCuenta" runat="server" Text="Sub Cuenta"></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="TxtSubCuenta" runat="server"></telerik:radtextbox>
                </td>
                <td>
                    <asp:Label ID="LblSubSubCuenta" runat="server" Text="Sub Sub-Cta"></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="TxtSubSubCuenta" runat="server"></telerik:radtextbox>
                </td>
                <%--   <td style="display:none">
                    <asp:Label ID="LblCuentaPago" runat="server" Text="Cta Pago"></asp:Label>
                </td>
                <td style="display:none">
                    <telerik:RadTextBox ID="txtCuentaPago" runat="server"></telerik:RadTextBox>
                </td>--%>
                <td>
                    <asp:Label ID="LblCuentaPago" runat="server" Text="Cta Pago"></asp:Label>
                </td>
                <td>
                    <telerik:radtextbox id="txtCuentaPago" runat="server"></telerik:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBox ID="chkFlete" runat="server" Text="Flete" AutoPostBack="true" Checked="false" />
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkNoInventariable" runat="server" Text="No Inventariable" AutoPostBack="true"
                        Checked="false" />
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkCompraLocal" runat="server" Text="Compra Local" AutoPostBack="true"
                        Checked="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBox ID="chkServicios" runat="server" Text="Servicios" AutoPostBack="true"
                        Checked="false" />
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkOtros" runat="server" Text="Otros" AutoPostBack="true" Checked="false" />
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkHonorarios" runat="server" Text="Honorarios" AutoPostBack="true"
                        Checked="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBox ID="chkArrendamientos" runat="server" Text="Arrendamientos" AutoPostBack="true"
                        Checked="false" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="rgCuenta" runat="server" autogeneratecolumns="False" gridlines="None"
                        onneeddatasource="rgCuenta_NeedDataSource" onitemcommand="rgCuenta_ItemCommand"
                        onpageindexchanged="rgCuenta_PageIndexChanged" pagesize="12" allowpaging="True"
                        mastertableview-nomasterrecordstext="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElecCuenta" HeaderText="Clave" UniqueName="Id_PagElecCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_Descripcion" HeaderText="Descripción" UniqueName="PagElecCuenta_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_Numero" HeaderText="Numero" UniqueName="PagElecCuenta_Numero"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_CC" HeaderText="CC" UniqueName="PagElecCuenta_CC"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_SubCuenta" HeaderText="Sub-Cta" UniqueName="PagElecCuenta_SubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_SubSubCuenta" HeaderText="SubSub-Cta" UniqueName="PagElecCuenta_SubSubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_CuentaPago" HeaderText="Cuenta Pago" UniqueName="PagElecCuenta_CuentaPago" Visible="true"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn UniqueName="Flete" DataField="Flete" HeaderText="Flete" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="NoInventariable" DataField="NoInventariable" HeaderText="No Inventariable" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="CompraLocal" DataField="CompraLocal" HeaderText="Compra Local" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="Servicios" DataField="Servicios" HeaderText="Servicios" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="Otros" DataField="Otros" HeaderText="Otros" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="Honorarios" DataField="Honorarios" HeaderText="Honorarios" visible = "true"/>  
                            <telerik:GridCheckBoxColumn UniqueName="Arrendamientos" DataField="Arrendamientos" HeaderText="Arrendamientos" visible = "true"/>  

                           
                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:radgrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ID" runat="server" />
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

        </script>
    </telerik:radcodeblock>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ProAcreedor_Autorizacion.aspx.cs" Inherits="SIANWEB.ProAcreedor_Autorizacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
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
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgAcreedor" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="rgAcreedor_NeedDataSource" OnItemCommand="rgAcreedor_ItemCommand" OnPageIndexChanged="rgAcreedor_PageIndexChanged"
                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Acr" HeaderText="Clave" UniqueName="Id_Acr">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Nombre" UniqueName="Acr_Nombre">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Calle" HeaderText="Calle" UniqueName="Acr_Calle">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Numero" HeaderText="Numero" UniqueName="Acr_Numero">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_NumInterior" HeaderText="Num. Int." UniqueName="Acr_NumInterior">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_CP" HeaderText="CP" UniqueName="Acr_CP">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Colonia" HeaderText="Colonia" UniqueName="Acr_Colonia">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Municipio" HeaderText="Municipio" UniqueName="Acr_Municipio">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Estado" HeaderText="Estado" UniqueName="Acr_Estado">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_RFC" HeaderText="RFC" UniqueName="Acr_RFC">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Número Acreedor" UniqueName="Acr_NumeroGenerado">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" CausesValidation="true" MaxLength="50" runat="server"  
                                        AutoPostBack="true" Style="text-transform: uppercase;">  
                                        <ClientEvents OnKeyPress="OnKeyPress" />  
                                    </telerik:RadTextBox>
                                    <asp:RegularExpressionValidator ID="RfvTxtNumeroAcreedor" 
                                                    runat="server" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="*Invalido" 
                                                    ForeColor="Red" 
                                                    ValidationExpression="^([a-zñA-ZÑ\x26]{3}([0-9]{4}))?$" 
                                                    ControlToValidate="TxtNumeroAcreedor">
                                    </asp:RegularExpressionValidator>
                                    <%--<telerik:RadNumericTextBox ID="TxtNumeroAcreedor" runat="server"></telerik:RadNumericTextBox>--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Acr_Banco" HeaderText="Banco" UniqueName="Acr_Banco">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Cuenta" HeaderText="CLABE" UniqueName="Acr_Cuenta">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea autorizar el acreedor?</br></br>" Text="Autorizar"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                             <telerik:GridBoundColumn DataField="Acr_Tipo" HeaderText="Acr_Tipo" UniqueName="Acr_Tipo" Visible="false">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function OnKeyPress(sender, args) {
            args.set_newValue(args.get_newValue().toUpperCase());
        }  
    </script> 
</asp:Content>

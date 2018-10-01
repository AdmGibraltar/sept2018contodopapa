<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatAcreedor.aspx.cs" Inherits="SIANWEB.CatAcredor" %>

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
        <table  style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="LblClave" runat="server" text="Clave"></asp:Label>
                </td>
                <td>
                    <telerik:radnumerictextbox id="txtClave" runat="server" width="70px" minvalue="1" AutoPostBack="true" OnTextChanged="txtClave_TextChanged">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        <ClientEvents OnKeyPress="handleClickEvent" />
                    </telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="LblNombre" runat="server" text="Nombre"></asp:Label>
                </td>
                <td colspan="5">
                    <telerik:RadTextBox ID="TxtNombre" runat="server" width="200px"></telerik:RadTextBox>                    
                    <asp:RequiredFieldValidator ID="RfvNombre" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
               
                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgBuscar_Click"
                            ToolTip="Buscar"    />
                 </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblCalle" runat="server" Text="Calle"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtCalle" runat="server" width="200px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvCalle" runat="server" ControlToValidate="TxtCalle" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="LblNumero" runat="server" Text="Numero"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtNumero" runat="server" width="80px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvNumero" runat="server" ControlToValidate="TxtNumero" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="LblNumeroInterior" runat="server" Text="Interior"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtNumeroInterior" runat="server" width="80px"></telerik:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="LblCp" runat="server" Text="CP"></asp:Label>
                </td>
                <td>
                    <%--<telerik:RadTextBox ID="TxtCp" runat="server" width="50px"></telerik:RadTextBox>--%>
                    <telerik:RadNumericTextbox ID="TxtCp" runat="server" with="50px" MaxLength="6"><NumberFormat GroupSeparator="" DecimalDigits="0" /></telerik:RadNumericTextbox>
                    <asp:RequiredFieldValidator ID="RfvCp" runat="server" ControlToValidate="TxtCp" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblCorreo" runat="server" Text="Correo"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtCorreo" runat="server" width="200px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvCorreo" runat="server" ControlToValidate="TxtCorreo" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RfvCorreo2" 
                                                    runat="server" 
                                                    Display="Dynamic"
                                                    ErrorMessage="*Invalido" 
                                                    ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                    ControlToValidate="TxtCorreo">
                    </asp:RegularExpressionValidator>
                   
                </td>
                <td>
                    <asp:Label ID="LblTelefono" runat="server" Text="Telefono"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtTelefono" runat="server" width="80px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvTelefono" runat="server" ControlToValidate="TxtTelefono" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
              <%--  <td>
                    <asp:Label ID="LblContacto" runat="server" Text="Contacto"></asp:Label>
                </td>--%>
               <%-- <td colspan="3">
                    <telerik:RadTextBox ID="TxtContacto" runat="server" width="250px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvContacto" runat="server" ControlToValidate="TxtContacto" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblColonia" runat="server" Text="Colonia"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtColonia" runat="server" width="200px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvColonia" runat="server" ControlToValidate="TxtColonia" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="LblMunicipio" runat="server" Text="Municipio"></asp:Label>
                </td>
                <td colspan="5">
                    <telerik:RadTextBox ID="TxtMunicipio" runat="server" width="200px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvMunicipio" runat="server" ControlToValidate="TxtMunicipio" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblEstado" runat="server" Text="Estado"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtEstado" runat="server" width="200px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RfvEstado" runat="server" ControlToValidate="TxtEstado" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblRfc" runat="server" Text="RFC"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox OnTextChanged="TxtRfc_TextChanged" ID="TxtRfc" runat="server" width="100px" AutoPostBack="True">
                    </telerik:RadTextBox>
                    
                    <%--<telerik:RadTextBox ID="TxtRfc" runat="server" width="100px"></telerik:RadTextBox>--%>
                    <asp:RegularExpressionValidator ID="RfvRfc1" 
                                                    runat="server" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="*Invalido" 
                                                    ForeColor="Red" 
                                                    ValidationExpression="^([a-zñA-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([a-zA-Z\d]{3})?$" 
                                                    ControlToValidate="TxtRfc">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RfvRfc2" 
                                                runat="server" 
                                                ControlToValidate="TxtRfc" 
                                                Display="Dynamic" 
                                                ErrorMessage="*Requerido" 
                                                ForeColor="Red" 
                                                ValidationGroup="guardar">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="LblEstatus" runat="server" Text="Estatus:"></asp:Label>
                    <asp:Label ID="lblAutorizado" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="3">
                      <asp:CheckBox ID="rdActivo" runat="server" Text="" 
                     AutoPostBack="true" Checked = "false" />
                </td>
               

            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td>
                    <telerik:RadCombobox id="cmbTipo" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="0" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Proveedor" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Acreedor" Value="2" /></Items>
                    </telerik:RadCombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="cmbTipo" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="LblCondPago" runat="server" Text="Condiciones de Pago"></asp:Label>
                </td>
                <td>
                    <%--<telerik:RadTextBox ID="" runat="server" width="80px"></telerik:RadTextBox>--%>
                    <telerik:RadNumericTextbox ID="TxtCondPago" runat="server" with="80px"><NumberFormat DecimalDigits="0" /></telerik:RadNumericTextbox>
                    <asp:RequiredFieldValidator ID="RfvCondPago" runat="server" ControlToValidate="TxtCondPago" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                 <%--JFCV 13 ene 2016 agregar banco y clave bancaria --%>
                <td>
                    <asp:Label ID="lblBanco" runat="server" Text="Banco"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtBanco" runat="server" with="80px"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldBanco" runat="server" ControlToValidate="TxtBanco" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:Label ID="lblCuenta" runat="server" Text="CLABE "></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="TxtCuentaClabe" runat="server" with="80px" MaxLength="18"  MinLength="18" ></telerik:RadTextBox>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatorClabe" 
                                                    runat="server" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="*Invalido" 
                                                    ForeColor="Red" 
                                                    ValidationExpression="^([0-9]{18})?$" 
                                                    ControlToValidate="TxtCuentaClabe">
                    </asp:RegularExpressionValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldCuenta" runat="server" ControlToValidate="TxtCuentaClabe" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>

                
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
                            <telerik:GridBoundColumn DataField="Acr_Tipo" HeaderText="Tipo" UniqueName="Acr_Tipo" Visible="false">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="LblTipoDescripcion" runat="server" Text='<%# Eval("Acr_Tipo").ToString() == "1" ? "Proveedor" : "Acreedor" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
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
                            <telerik:GridBoundColumn DataField="Acr_Telefono" HeaderText="Telefono" UniqueName="Acr_Telefono">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Correo" HeaderText="Correo" UniqueName="Acr_Correo">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Contacto" HeaderText="Contacto" UniqueName="Acr_Contacto" visible = "false">
                                 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_CondPago" HeaderText="Cond. Pago" UniqueName="Acr_CondPago">
                                <HeaderStyle Width="60" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_RFC" HeaderText="RFC" UniqueName="Acr_RFC">
                                <HeaderStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <%--JFCV 13 enero 2016 agregar el banco y la cuenta bancaria --%>
                             <telerik:GridBoundColumn DataField="Acr_Banco" HeaderText="Banco" UniqueName="Acr_Banco">
                                <HeaderStyle Width="60" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Acr_Cuenta" HeaderText="CLABE" UniqueName="Acr_Cuenta">
                                <HeaderStyle Width="60" />
                            </telerik:GridBoundColumn>

                           <%-- <telerik:GridBoundColumn DataField="Acr_Autorizado" HeaderText="Autorizado" UniqueName="Acr_Autorizado" visible = "false">
                             </telerik:GridBoundColumn> --%>

                             <telerik:GridCheckBoxColumn UniqueName="Acr_Autorizado" DataField="Acr_Autorizado" HeaderText="Autorizado" visible = "false">
                            </telerik:GridCheckBoxColumn>  

                            <telerik:GridCheckBoxColumn UniqueName="Acr_Estatus" DataField="Acr_Estatus"  HeaderText="Activo" visible = "false">
                            </telerik:GridCheckBoxColumn>  

                            <telerik:GridBoundColumn DataField="Acr_EstatusDescripcion" HeaderText="Estatus" UniqueName="Acr_EstatusDescripcion" visible = "true">
                             </telerik:GridBoundColumn>



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
                </telerik:RadGrid>
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

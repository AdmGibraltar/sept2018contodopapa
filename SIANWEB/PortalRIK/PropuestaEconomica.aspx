<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="PropuestaEconomica.aspx.cs" Inherits="SIANWEB.PortalRIK.PropuestaEconomica" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {

                    case 'save':
                        //select tab datos generales
                        
                        break;
                }
                if (continuarAccion == true) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }
                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                GetRadWindow().Close();
            }

            function RowDeleted(sender, eventArgs) {
            }
        </script>
    </telerik:RadCodeBlock>
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
    </div>
    <div>
        <telerik:RadGrid ID="rgPropuesta" runat="server" GridLines="None" AllowPaging="False" AutoGenerateColumns="False" OnNeedDataSource="rgPropuesta_NeedDataSource" OnUpdateCommand="rgPropuesta_UpdateCommand">
            <MasterTableView Name="Master" CommandItemDisplay="Top" EditMode="InPlace" HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros." DataKeyNames="Id_Prd">
                <Columns>
                    <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Código" UniqueName="codigo" ReadOnly="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="descripcion" ReadOnly="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Prd_Pres" HeaderText="Presentacion" UniqueName="presentacion" ReadOnly="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Prd_Precio" HeaderText="Precio" UniqueName="precio" ReadOnly="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PropEc_Dilucion" HeaderText="Dilución" UniqueName="dilucion">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PropEc_CostoUso" HeaderText="Costo en Uso" UniqueName="costoEnUso">
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridEditCommandColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnRowDeleted="RowDeleted" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>

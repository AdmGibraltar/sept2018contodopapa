<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviaCorreoInstitucional.aspx.cs" Inherits="SIANWEB.EnviaCorreoInstitucional" Culture="es-MX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script type="text/javascript" >
function handleClickEvent(sender, eventArgs) {
        var key = eventArgs.get_keyCode();
        if (key && key == 13)
            eventArgs.set_cancel(true);
    }

</script>
<body>
<form id="form1" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadAjaxManager ID="RAM1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="form1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"
                    UpdatePanelHeight="" />                                                       
            </UpdatedControls>
        </telerik:AjaxSetting>            
    </AjaxSettings>
</telerik:RadAjaxManager>
    <table style="font-family: Verdana; font-size: 8pt;" border="0" width="96%">
        <tr>
            <td colspan="5">&nbsp;
                <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblMensaje" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>                                        
                        <td style="padding-right:5px">
                            <asp:Button ID="btnEditaMail" runat="server" Text="Editar" Enabled="false" OnClick="btnEditaMail_OnClick" Visible="false"/>
                        </td>
                        <td style="padding-right:5px">
                            <asp:Button ID="btnEnviaMail" runat="server" Text="Enviar"  OnClick="btnEnviaMail_OnClick" />
                        </td>
                        <td style="padding-right:5px">
                            <asp:Button ID="btnCancelaMail" runat="server" Text="Cancelar" OnClientClick="javascript:window.close();"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="3">&nbsp;
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                <asp:HiddenField ID="hf_CitaVisitaModif" runat="server" />
                <asp:HiddenField ID="HF_Usuario" runat="server" /></td></tr>
    </table>
    <hr align="left" noshade="noshade" size="2" width="96%" />
    <table style="font-family: Verdana; font-size: 8pt;" border="0" width="90%">
        <tr>
            <td colspan="5">&nbsp;
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>                                        
                        <td style="padding-right:5px"><b>De:</b></td>
                        <td><asp:Label ID="lblFrom" runat="server" ></asp:Label></td>
                    </tr>
                    <tr>                                        
                        <td style="padding-right:5px"><b>Para:</b></td>
                        <td><asp:Label ID="lblTo" runat="server" ></asp:Label></td>
                    </tr>
                    <tr>                                        
                        <td style="padding-right:5px"><b>Asunto:</b></td>
                        <td><asp:Label ID="lblSubject" runat="server" ></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding-right:5px"><img id=logokey src="../Imagenes/logo.jpg" />
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td style="padding-right:5px"> Hola <b><asp:Label ID="lblContacoEmpresa" runat="server" ></asp:Label></b>:</td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Espero que este email te encuentre bien! Te escribo ya que dentro de Key se esta implementando una nueva campaña de acercamiento con sus clientes.
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Key tiene una nueva plataforma (<i>SIANWeb4Costumers</i>) que podría ayudar a <b><asp:Label ID="lblEmpresa" runat="server" ></asp:Label></b> para lograr mejores beneficios, entre los cuales podemos listar:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ul style="list-style-type:square">
                                <li>Envio y Recepcion de Pedidos al momento.</li>
                                <li>Seguimiento y Notificaciones del estatus del pedido, factura, nota de credito, etc.</li>
                                <li>Impresion de Facturas desde la aplicacion para celulares y tablets</li>
                                <li>Timbrado de incidencias sobre pedidos incompletos, sustituidos, cancelados, etc.</li>
                                <li>Confirmacion de entregas y seguimiento en ruta de los despachos.</li>
                            </ul>
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Podriamos explorar estos beneficios que <i>SIANWeb4Costumers</i> brindaria especificamente a tu negocio si tienes tiempo para una llamada o incluso una visita personal en los siguientes dias.
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Quedo al pendiente de cualquier duda, comentario para con gusto ampliar la informacion.
                        </td>
                    </tr>
                     <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Saludos,
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
            

    </table>
</form>
</body>
</html>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;

namespace CANCELAWEBSERVICE
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }



        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {



            string accionError = string.Empty;
            string mensajeError = string.Empty;


            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Cancelar":


                        string valorResultadoCancelacion = "0";
                        WS_CANCELACION.Service1 ws = new WS_CANCELACION.Service1();
                        String respuestaCancelacion = ws.CancelacionWS("" + RadRFC.Text + "," + RadUUI.Text + "");
                        XmlDocument XmlCancelacion = new XmlDocument();
                        XmlCancelacion.LoadXml(respuestaCancelacion);


                        foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                        {
                            if (nodo.Name == "Acuse")
                            {
                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                {
                                    if (Nodo_nivel2.Name == "Folios")
                                    {
                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                        {
                                            if (Nodo_nivel3.Name == "EstatusUUID")
                                            {
                                                valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                            }

                                        }

                                    }
                                }

                            }
                        }
                        string valorResultadoCancelacionTexto = string.Empty;
                        switch (valorResultadoCancelacion)
                        {
                            case "201":
                                valorResultadoCancelacionTexto = "Cancelado Exitosamente";
                                break;
                            case "202":
                                valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                                break;
                            case "203":
                                valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                                break;
                            case "204":
                                valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                                break;
                            case "205":
                                valorResultadoCancelacionTexto = "Documento No Existe emisión";
                                break;
                            case "301":
                                valorResultadoCancelacionTexto = "XML mal formado";
                                break;
                            case "302":
                                valorResultadoCancelacionTexto = "Sello mal formado o inválido";
                                break;
                            case "303":
                                valorResultadoCancelacionTexto = "Sello no corresponde a emisor caduco";
                                break;
                            case "304":
                                valorResultadoCancelacionTexto = "Certificado revocado o caduco";
                                break;
                            case "305":
                                valorResultadoCancelacionTexto = "La Fecha de emisión no esta dentro de la vigencia del CSD del Emisor";
                                break;
                            case "306":
                                valorResultadoCancelacionTexto = "El certificado no es de tipo CSD";
                                break;
                            case "307":
                                valorResultadoCancelacionTexto = "El CFDI contiene un timbre previo";
                                break;
                            case "308":
                                valorResultadoCancelacionTexto = "Certificado no expedido por el SAT";
                                break;
                            default:
                                valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                                break;
                        }

                        respuestaCancelacion = HttpUtility.JavaScriptStringEncode(respuestaCancelacion);

                        if (valorResultadoCancelacion == "201")
                        {
                            string script = "var newWindow = window.open('','_blank','toolbar=0, location=0, directories=0, status=0, scrollbars=1, resizable=1, copyhistory=1, menuBar=1, width=640, height=480, left=50, top=50', true);" +
                            "var preEl = newWindow.document.createElement('pre'); " +
                            "var codeEl = newWindow.document.createElement('code'); " +
                            " codeEl.appendChild(newWindow.document.createTextNode(\"" + respuestaCancelacion + "\"));" +
                            "preEl.appendChild(codeEl);" +
                            "newWindow.document.body.appendChild(preEl);";

                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "controlJSScript", script, true);
                        }
                        else
                        {
                            this.Alerta(valorResultadoCancelacionTexto);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }


        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }



        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                //  ErrorManager(ex, "Alerta");
            }
        }

        protected void RadAjaxManager2_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }



    }
}

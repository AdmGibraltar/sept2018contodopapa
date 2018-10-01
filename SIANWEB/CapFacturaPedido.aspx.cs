using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;

namespace SIANWEB
{
    public partial class CapFacturaPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    Session["ListaRemisionesFactura" + Session.SessionID] = null;
                    Session["PedidoFacturacion" + Session.SessionID] = null;
                    CN_CapFactura fac = new CN_CapFactura();
                    string[] datosGen = fac.ConsultaFacturacion_DatosGeneralesFacturacion(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver);

                    lblEmpresaNombre.Text = datosGen[0];
                    lblSucursalNombre.Text = datosGen[1];
                    lblRegionNombre.Text = datosGen[2];
                    txtPedido.Focus();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();

                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                if (!cn_capPedido.ConsultaPedidoFacturacion(ref pedido, sesion.Emp_Cnx))//pedido no existe
                {
                    this.DisplayMensajeAlerta("PedidoNoExiste");
                }
                else
                {
                    if (pedido.Estatus != "A" && pedido.Estatus != "F" && pedido.Estatus != "R" && pedido.Estatus != "X")//si el pedido no ha sido asignado o autorizado
                    {
                        //if (pedido.Estatus != "X")
                        //{
                            this.DisplayMensajeAlerta("PedidoEstatusNoValido");
                        //}
                        //else
                        //{
                        //    this.DisplayMensajeAlerta("PedidoProductoRemisionFacturado");
                        //}
                    }
                    else
                    {
                        if (cn_capPedido.ConsultaPedidoTieneUnidadesRemisionadas(ref pedido, sesion.Emp_Cnx))
                        {
                            //this.DisplayMensajeAlerta("PedidoProductoRemisionado");
                            Session["PedidoRemisionado" + Session.SessionID] = "1";
                            Session["PedidoFacturacion" + Session.SessionID] = txtPedido.Text;
                            string mensaje = "Este pedido tiene producto remisionado";
                            //cerrar ventana radWindow de detalle de factura-pedido
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaPedido('", mensaje, "')"));
                        }
                        else
                        { 
                            if (pedido.Estatus == "F" && pedido.Cant_Facturada <= 0)
                            {
                                this.DisplayMensajeAlerta("PedidoProductoFacturado");
                            }
                            else
                            {
                                Session["PedidoFacturacion" + Session.SessionID] = txtPedido.Text;
                                string mensaje = string.Empty;
                                //cerrar ventana radWindow de detalle de factura-pedido
                                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaPedido('", mensaje, "')"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat("PedidoErrorConsulta", ex.Message));
            }
        }

        #endregion

        #region Funciones

        private void DisplayMensajeAlerta(string mensaje)
        {
            string Id = txtPedido.ClientID;
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("PedidoEstatusNoValido"))
                    AlertaFocus("El pedido no tiene un estatus válido, no ha sido asignado", Id);
                else
                    if (mensaje.Contains("PedidoNoExiste"))
                        AlertaFocus("El pedido no existe", Id);
                    else
                        if (mensaje.Contains("PedidoErrorConsulta"))
                            Alerta("Ocurrió un error al consultar el pedido");
                        else
                            if (mensaje.Contains("PedidoProductoRemisionado"))
                                AlertaFocus("Este pedido tiene producto remisionado", Id);
                            else
                                if (mensaje.Contains("PedidoProductoFacturado"))
                                    AlertaFocus("El pedido ya ha sido facturado", Id);
                                else
                                    if (mensaje.Contains("PedidoProductoRemisionFacturado"))
                                        AlertaFocus("Este pedido ya ha sido remisionado y facturado", Id);
                                    else
                                        AlertaFocus(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")), Id);
        }

        #endregion

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion
    }
}
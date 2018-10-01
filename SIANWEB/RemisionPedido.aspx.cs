using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;
using CapaNegocios;

namespace SIANWEB
{
    public partial class RemisionPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RadNumericTextBoxPedido.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
               
                //int PedRem=-1;
                //int.TryParse(Request.QueryString["PedRem"], out PedRem);
                int id_ped = int.Parse(RadNumericTextBoxPedido.Text);
                Pedido pedido=new Pedido();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Ped = id_ped;
                new CN_CapPedido().ConsultaPedido(ref pedido, sesion.Emp_Cnx); //new CN_CapPedido().ConsultaPedidoDet(pedido, ref tabla, sesion.Emp_Cnx);
                if (pedido.Id_Cte==0 && pedido.Ped_Importe==0 && pedido.Id_Ter==0 )
                {
                    lblMsg.Text= "No existe el pedido a remisionar";
                    //Alerta("No existe el pedido a remisionar");
                    RadNumericTextBoxPedido.Text = "";
                    //cerrar ventana
                    //RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "No existe el pedido a remisionar", "')"));
                    return;                    
                }
                else
                {//si existe pedido
                    List<string> statusPosibles = new List<string>() { "A", /*"U",*/ "X", "F", "R" }; 
                    if (pedido.Estatus==null || !statusPosibles.Contains(pedido.Estatus.ToUpper()))
                    {
                        lblMsg.Text = "El documento se encuentra en estatus no válido";
                        //Alerta("El documento se encuentra en estatus no válido");
                        RadNumericTextBoxPedido.Text = "";
                        return;
                    }
                    RadNumericTextBoxPedido.Text = "";
                    RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow_ventanaRemisionPedido('", id_ped.ToString(), "')"));
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
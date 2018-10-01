using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB
{
    public partial class ObtenerNombreCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string valor_retorno = "";

                if (Request.Params["ini"] != null || Sesion == null)
                {
                    valor_retorno = "-0";
                }
                else
                {
                    try
                    {
                        int Cte = int.Parse(Request.Params["cte"]);
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        Clientes clientes = new Clientes();
                        clientes.Id_Emp = sesion.Id_Emp;
                        clientes.Id_Cd = sesion.Id_Cd_Ver;
                        clientes.Id_Cte = Cte;
                        clientes.Ignora_Inactivo = false;
                        CN_CatCliente clsCliente = new CN_CatCliente();
                        try
                        {
                            clsCliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);
                            valor_retorno = clientes.Cte_NomComercial;
                        }
                        catch (Exception ex)
                        {
                            valor_retorno = "-1@@" + ex.Message;
                        }
                    }
                    catch
                    {
                        valor_retorno = "";
                    }
                }
                
                Response.Write(valor_retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
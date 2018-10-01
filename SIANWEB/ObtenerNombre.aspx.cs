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
    public partial class ObtenerNombre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string valor_retorno = "";

                if (Sesion == null)
                {
                    valor_retorno = "-0";
                }
                else
                {

                    int Prd = Convert.ToInt32(Request.Params["prd"]);

                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    Producto producto = new Producto();
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    try
                    {
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Prd,0);
                        if (Request.Params["Bi"] != null)
                        {
                            if (!(bool)producto.Prd_AparatoSisProp)
                            {
                                valor_retorno = "-2";
                            }
                            else
                            {
                                valor_retorno = producto.Prd_Descripcion;
                            }
                        }
                        else
                        {
                            valor_retorno = producto.Prd_Descripcion;
                        }
                    }
                    catch (Exception ex)
                    {
                        valor_retorno = ex.Message;
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
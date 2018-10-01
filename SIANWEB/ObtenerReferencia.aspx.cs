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
    public partial class ObtenerReferencia : System.Web.UI.Page
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
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        int doc = 0;
                        int tm = 0;
                        try
                        {
                            doc = int.Parse(Request.Params["doc"]);
                            tm = int.Parse(Request.Params["tm"]);
                        }
                        catch
                        {
                            valor_retorno = "-1";
                        }

                        int cte = Request.Params["cte"] == "" ? -1 : int.Parse(Request.Params["cte"]);
                        CN_CatRemision cn_rem = new CN_CatRemision();
                        string verificador = "";
                        try
                        {
                            cn_rem.ConsultarReferencia(Sesion, doc, tm, ref verificador, cte);
                            valor_retorno = verificador.ToString();
                        }
                        catch (Exception ex)
                        {
                            valor_retorno = ex.Message;
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
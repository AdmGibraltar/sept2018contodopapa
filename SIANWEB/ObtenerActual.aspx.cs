using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;

namespace SIANWEB
{
    public partial class ObtenerActual : System.Web.UI.Page
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

                    Funciones funcion = new Funciones();


                    int? Cte = Request.Params["cte"].ToString() == "" ? (int?)null : Convert.ToInt32(Request.Params["cte"]);
                    int? Prd = Request.Params["prd"] == "" ? (int?)null : Convert.ToInt32(Request.Params["prd"]);
                    int? Terr = Request.Params["terr"] == "" ? (int?)null : Convert.ToInt32(Request.Params["terr"]);
                    string mov = Request.Params["mov"].ToString();

                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    valor_retorno = CN_Comun.Actual(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, Cte, Prd, Terr, mov, funcion.GetLocalDateTime(Sesion.Minutos));

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
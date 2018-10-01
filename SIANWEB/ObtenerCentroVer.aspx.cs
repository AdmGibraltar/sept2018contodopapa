using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class ObtenerCentroVer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string Cd_Ver = Request.Params["cd"].ToString();

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion != null)
                {
                    sesion.Id_Cd_Ver = Convert.ToInt32(Cd_Ver);

                    CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
                    Calendario calendario = new Calendario();
                    cn_catcalendario.ConsultaCalendarioActual(ref calendario, sesion);


                    sesion.CalendarioIni = calendario.Cal_FechaIni;
                    sesion.CalendarioFin = calendario.Cal_FechaFin;

                    Response.Write("OK");
                }
                else
                {
                    Response.Write("NO");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
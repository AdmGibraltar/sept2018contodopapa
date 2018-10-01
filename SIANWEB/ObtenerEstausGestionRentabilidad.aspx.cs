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
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Configuration;



namespace SIANWEB
{
    public partial class ObtenerEstausGestionRentabilidad : System.Web.UI.Page
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


                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                    int Id_Emp = Sesion.Id_Emp;
                    int Id_Cd = Sesion.Id_Cd_Ver;
                    int Id_Ter = Convert.ToInt32(Request.Params["Id_Ter"]);
                    int Id_Cte = Convert.ToInt32(Request.Params["Id_Cte"]);
                    int mesInicial = Convert.ToInt32(Request.Params["mesInicial"]);
                    int anioInicial = Convert.ToInt32(Request.Params["anioInicial"]);
                    int mesFinal = Convert.ToInt32(Request.Params["mesFinal"]);
                    int anioFinal = Convert.ToInt32(Request.Params["anioFinal"]);

                    new CN_GestionRentabilidadSimulador().ObtieneEstatusSGR(Id_Emp
                                                              , Id_Cd
                                                              , Id_Ter
                                                              , Id_Cte
                                                              , mesInicial
                                                              , anioInicial
                                                              , mesFinal
                                                              , anioFinal
                                                              , Sesion.Id_U
                                                              , Sesion.Emp_Cnx
                                                              ,ref valor_retorno);




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
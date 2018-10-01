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
    public partial class CapFacturaAlmacenCobro_Seleccionar : System.Web.UI.Page
    {
        private List<FacturaAlmacenCobroDet> ListaFacturaRevisionCobro
        {
            get { return (List<FacturaAlmacenCobroDet>)Session[Session.SessionID + "ListaFacturaAlmacenCobroDet"]; }
            set { Session[Session.SessionID + "ListaFacturaAlmacenCobroDet"] = value; }
        }

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
                    int doc = Convert.ToInt32(Request.Params["doc"]);
                    bool sel = Convert.ToBoolean(Request.Params["sel"]);

                    if (doc != -1)
                    {
                        ListaFacturaRevisionCobro.Where(FacturaRevisionCobroDet => FacturaRevisionCobroDet.Fac_Doc == doc).ToList()[0].Fac_Seleccionado = sel;

                        int seleccionados = ListaFacturaRevisionCobro.Where(FacturaRevisionCobroDet => FacturaRevisionCobroDet.Fac_Seleccionado == true).ToList().Count;
                        int total = ListaFacturaRevisionCobro.Count;

                        if (seleccionados == total)
                        {
                            valor_retorno = "1";
                        }
                        else
                        {
                            valor_retorno = "2";
                        }
                    }
                    else
                    {
                        foreach (FacturaAlmacenCobroDet f in ListaFacturaRevisionCobro)
                        {
                            f.Fac_Seleccionado = sel;
                        }
                        valor_retorno = "3";
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
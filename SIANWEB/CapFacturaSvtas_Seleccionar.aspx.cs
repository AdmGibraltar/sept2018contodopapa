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
    public partial class CapFacturaSvtas_Seleccionar : System.Web.UI.Page 
    {
        private List<FacturaSvtaAlmacenDet> ListaFacturaSvtaAlmacen
        {
            get { return (List<FacturaSvtaAlmacenDet>)Session[Session.SessionID + "ListaFacturaSvtaAlmacenDet"]; }
            set { Session[Session.SessionID + "ListaFacturaSvtaAlmacenDet"] = value; }
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
                        ListaFacturaSvtaAlmacen.Where(FacturaSvtaAlmacenDet => FacturaSvtaAlmacenDet.Fva_Doc == doc).ToList()[0].Fva_Seleccionado = sel;

                        int seleccionados = ListaFacturaSvtaAlmacen.Where(FacturaSvtaAlmacenDet => FacturaSvtaAlmacenDet.Fva_Seleccionado == true).ToList().Count;
                        int total = ListaFacturaSvtaAlmacen.Count;

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
                        foreach (FacturaSvtaAlmacenDet f in ListaFacturaSvtaAlmacen)
                        {
                            f.Fva_Seleccionado = sel;
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
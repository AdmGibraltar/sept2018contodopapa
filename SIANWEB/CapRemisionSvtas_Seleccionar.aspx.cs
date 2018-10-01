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
    public partial class CapRemisionSvtas_Seleccionar : System.Web.UI.Page 
    {
        private List<RemisionSvtaAlmacenDet> ListaRemisionSvtaAlmacen 
        {
            get { return (List<RemisionSvtaAlmacenDet>)Session[Session.SessionID + "ListaRemisionSvtaAlmacenDet"]; }
            set { Session[Session.SessionID + "ListaRemisionSvtaAlmacenDet"] = value; }
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
                        ListaRemisionSvtaAlmacen.Where(RemisionSvtaAlmacenDet => RemisionSvtaAlmacenDet.Rva_Doc == doc).ToList()[0].Rva_Seleccionado = sel;

                        int seleccionados = ListaRemisionSvtaAlmacen.Where(RemisionSvtaAlmacenDet => RemisionSvtaAlmacenDet.Rva_Seleccionado == true).ToList().Count;
                        int total = ListaRemisionSvtaAlmacen.Count;

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
                        foreach (RemisionSvtaAlmacenDet f in ListaRemisionSvtaAlmacen)
                        {
                            f.Rva_Seleccionado = sel;
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
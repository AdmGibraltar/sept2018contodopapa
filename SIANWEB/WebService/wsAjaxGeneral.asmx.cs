using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SIANWEB.WebService
{
    /// <summary>
    /// Summary description for wsAjaxGeneral
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAjaxGeneral : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld() {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string LLenaCveTerritorio(string HF_ID, int TipoRep, string txtUen, string txtSegmento, string txtTipoCliente) {
            string txtClave = "";

            if (HF_ID == "")
            {
                string claveTerritorio = string.Empty;
                txtClave = claveTerritorio;

                claveTerritorio += TipoRep;

                if (txtUen != string.Empty)
                { claveTerritorio += txtUen.PadLeft(2, '0'); }

                if (txtSegmento != string.Empty)
                {

                    List<CapaEntidad.Segmentos> List = new List<CapaEntidad.Segmentos>();
                    CapaNegocios.CN_CatSegmentos clsCatSegmentos = new CapaNegocios.CN_CatSegmentos();
                    CapaEntidad.Sesion session2 = new CapaEntidad.Sesion();
                    session2 = (CapaEntidad.Sesion)Session["Sesion" + Session.SessionID];
                    clsCatSegmentos.ConsultaSegmentos(session2.Id_Emp, Convert.ToInt32(txtSegmento), session2.Emp_Cnx, ref List);
                    claveTerritorio += Convert.ToString(List[0].Seg_IdXUen).Trim().PadLeft(2, '0');              
                
                }

                if (txtTipoCliente != string.Empty) { 
                    claveTerritorio += txtTipoCliente.PadLeft(2, '0'); 
                }

                CapaEntidad.Sesion Sesion = new CapaEntidad.Sesion();

                Sesion = (CapaEntidad.Sesion)Context.Session["Sesion" + Context.Session.SessionID];
                if (Sesion == null)
                    return "";

                claveTerritorio += MaximoId(claveTerritorio, TipoRep, txtTipoCliente, txtUen, txtSegmento, Sesion);
                txtClave = claveTerritorio;
            }

            return txtClave;
        }

        private string MaximoId(string prefix, int TipoRep, string txtTipoCliente, string txtUen, string txtSegmento, CapaEntidad.Sesion Sesion)
        {
            string Result = null;
            try {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                int TipoCliente = txtTipoCliente.Length > 0 ? Convert.ToInt32(txtTipoCliente) : 0;
                int idUen = txtUen.Length > 0 ? Convert.ToInt32(txtUen) : 0;
                int idSeg = txtSegmento.Length > 0 ? Convert.ToInt32(txtSegmento) : 0;

                Result = CN_Comun.MaximoTerritorio(
                    Sesion.Id_Emp,
                    Sesion.Id_Cd_Ver, TipoRep, TipoCliente, idUen, idSeg,
                    Sesion.Emp_Cnx,
                    "spCatTerritorio_Maximo",
                    prefix
                );
            }
            catch (Exception ex) {
                throw ex;
            }

            return Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB.WebService
{
    public class CatSegmentoController : ApiController
    {
        [HttpGet]
        public IEnumerable<CatSegmento> Get(int idEmp, int idUen)
        {
            CN_CatSegmentos cnCatSegmento = new CN_CatSegmentos();
            var resultado = cnCatSegmento.ObtenerSegmentosPorUen(idEmp, idUen, Sesion);
            return resultado;
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
    }
}
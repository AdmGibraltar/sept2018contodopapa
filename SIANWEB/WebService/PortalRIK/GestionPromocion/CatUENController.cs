using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CatUENController : ApiController
    {
        [HttpGet]
        public IEnumerable<CatUEN> Get(int idEmp, int idCd, int idRik)
        {
            CN_CatUen cnCatUen = new CN_CatUen();
            var resultado = cnCatUen.ObtenerUENsDeRepresentante(idEmp, idCd, idRik, Sesion);
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
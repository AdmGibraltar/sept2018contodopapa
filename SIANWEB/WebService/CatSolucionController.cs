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
    public class CatSolucionController : ApiController
    {
        [HttpGet]
        public IEnumerable<CapaModelo.CatSolucion> Get(int idEmp, int idArea)
        {
            CN_CatSolucion cnCatSolucion = new CN_CatSolucion();
            var resultado = cnCatSolucion.ObtenerPorEmpresaYArea(idEmp, idArea, Sesion);
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
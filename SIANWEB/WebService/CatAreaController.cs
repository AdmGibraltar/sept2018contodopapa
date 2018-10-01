using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatAreaController : ApiController
    {
        [HttpGet]
        public IEnumerable<Area> Get(int idEmp, int idSeg)
        {
            CN_CatArea cnCatArea = new CN_CatArea();
            List<Area> result = new List<Area>();
            Area parametros=new Area();
            parametros.Id_Emp = idEmp;
            parametros.Id_Seg = idSeg;
            cnCatArea.Lista(parametros, Sesion.Emp_Cnx, ref result);
            return result;
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
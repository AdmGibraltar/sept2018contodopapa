using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatUsuarioRikController: ApiController
    {
        [HttpGet]
        public  List<eUsuarioRik> Get(int IdGerente, int IdRik)
        {
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            CN_UsuarioRik cn = new CN_UsuarioRik();
            lst = cn.Lista(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
            return lst;
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
        //


    }
}
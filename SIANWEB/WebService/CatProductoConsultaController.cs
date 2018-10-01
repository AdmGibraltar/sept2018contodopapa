using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatProductoConsultaController : ApiController
    {
        [HttpGet]
        public Producto Get(int Id_Prd)
        {
            Producto P = new Producto();
            try
            {                
                CN_CatProducto cnProducto = new CN_CatProducto();
                cnProducto.Consulta_Producto(ref P, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Cd_Ver, Id_Prd, 0);
            }
            catch (Exception ex)
            {
                P = null;
            }
            return P;
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
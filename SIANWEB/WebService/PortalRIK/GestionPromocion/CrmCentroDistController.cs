using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using CapaDatos;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmCentroDistController : BaseWebAPIController
    {
        [HttpGet]
        public IEnumerable<CrmLista> Get()
        {
            List<CapaEntidad.CrmLista> lst = new List<CapaEntidad.CrmLista>();
            
            CN_CatCentroDistribucion CD = new CN_CatCentroDistribucion();
            CD.ConsultarCentroDistribucionCombo(1, Sesion.Id_Emp, 1, 0, ref lst, Sesion.Emp_Cnx);

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
    }
}
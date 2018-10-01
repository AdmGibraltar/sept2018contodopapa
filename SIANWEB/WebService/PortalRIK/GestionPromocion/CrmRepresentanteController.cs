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
    public class CrmRepresentanteController : BaseWebAPIController
    {
        [HttpGet]
        public IEnumerable<CrmLista> Get(int IdCD)
        {
            //
            // Se descontiuna el uso del parm. IdCD ya que se toma el actual.
            //

            List<CapaEntidad.CrmLista> lst = new List<CapaEntidad.CrmLista>();            
            CN_CatRepresentantes CR = new CN_CatRepresentantes();

            CR.Consultar_RepresentantesCombo(Sesion.Id_Emp, Sesion.Id_U, ref lst, Sesion.Emp_Cnx);
            //CR.ConsultarRepresentantesCombo(Sesion.Id_Cd, ref lst, Sesion.Emp_Cnx);

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
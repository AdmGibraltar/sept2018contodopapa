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

namespace SIANWEB.WebService
{
    public class CatPropuestaTecnoEconomicaEncController: ApiController
    {

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Encavezado
        [HttpGet]
        public CapaEntidad.eCapValProyecto Get(int CRM_Usuario_Rik, int Enc, int Id_Op, int Id_Cte, int Id_Val)
        {
            int Rik = 0;
            if (CRM_Usuario_Rik > 0)
            {
                Rik = CRM_Usuario_Rik;
            }
            else
            {
                Rik = Sesion.Id_Rik;
            }
            CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();
            CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
            Obj = cnPE.spCRMCapValProyecto(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Val, Sesion);
            return Obj;
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
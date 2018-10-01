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
    public class CatPropuestaTecnoEconomicaController: ApiController
    {
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Detalle
        [HttpGet]
        public List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> Get(int CRM_Usuario_Rik, int Id_Op, int Id_Cte, int Id_Val)
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
            List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
            CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
            lst = cnPE.CRM_ObtenerPropuestaEconomica(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Val, Sesion);
            return lst;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                        
        [HttpGet]
        public int Get(
            int Id_Op, int Id_Val, int Id_Cte, int Id_Prd, decimal Cantidad, 
            int AplDilucion, decimal DilucionA, decimal DilucionC, string CPT_ProductoActual, string CPT_SituacionActual, 
            string CPT_VentajasKey, string CPT_RecursoImagenProductoActual, string CPT_RecursoImagensolucionKey,
            decimal COP_CostoEnUso 
            )
        {
            int Result = 0;

            CN_CrmOportunidadesProductos OP = new CN_CrmOportunidadesProductos ();            
            Result = OP.Update_CrmOportunidadesProductos(
                Sesion.Id_Emp ,Sesion.Id_Cd,
                Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad, 
                AplDilucion, DilucionA, DilucionC,
                CPT_ProductoActual, CPT_SituacionActual, CPT_VentajasKey, CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
                COP_CostoEnUso,
                Sesion.Emp_Cnx);
            
            return Result;
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
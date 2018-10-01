using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmOportunidadesAplicacion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmOportunidadesAplicacion"></param>
        /// <param name="sesion"></param>
        public void Actualizar(int idOp, CrmOportunidadesAplicacion[] crmOportunidadesAplicaciones, Sesion sesion)
        {
            CD_CrmOportunidadesAplicacion cdCrmOportunidadesAplicacion = new CD_CrmOportunidadesAplicacion();
            cdCrmOportunidadesAplicacion.Actualizar(sesion.Id_Emp, sesion.Id_Cd, idOp, crmOportunidadesAplicaciones, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmOportunidadesAplicacion"></param>
        /// <param name="sesion"></param>
        public void Actualizar(CrmOportunidadesAplicacion crmOportunidadesAplicacion, Sesion sesion)
        {
            CD_CrmOportunidadesAplicacion cdCrmOportunidadesAplicacion = new CD_CrmOportunidadesAplicacion();
            cdCrmOportunidadesAplicacion.Actualizar(sesion.Id_Emp, sesion.Id_Cd, crmOportunidadesAplicacion.Id_Op, crmOportunidadesAplicacion.Id_Apl, crmOportunidadesAplicacion.CrmOpAp_VPO!=null ? crmOportunidadesAplicacion.CrmOpAp_VPO.Value : 0, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idCte"></param>
        /// <param name="idOp"></param>
        /// <returns></returns>
        public IEnumerable<CrmOportunidadesAplicacion> ObtenerPorOportunidad(Sesion sesion, int idCte, int idOp)
        {
            CD_CrmOportunidadesAplicacion cdCrmOportunidadesAplicacion = new CD_CrmOportunidadesAplicacion();
            var result = cdCrmOportunidadesAplicacion.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idOp, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="idCte"></param>
        /// <param name="idOp"></param>
        /// <returns></returns>
        public IEnumerable<CrmOportunidadesAplicacion> ObtenerPorOportunidad(Sesion sesion, int idCte, int idOp, IBusinessTransaction ibt)
        {
            CD_CrmOportunidadesAplicacion cdCrmOportunidadesAplicacion = new CD_CrmOportunidadesAplicacion();
            var result = cdCrmOportunidadesAplicacion.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idOp, ibt.DataContext);
            return result;
        }
    }
}

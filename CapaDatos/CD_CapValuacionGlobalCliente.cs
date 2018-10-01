using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapValuacionGlobalCliente
    {
        /// <summary>
        /// Inserta una nueva entrada en el repositorio CapValuacionGlobalCliente
        /// </summary>
        /// <param name="datos">Instancia de datos de la entidad CapValuacionGlobalCliente</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValuacionGlobalCliente</returns>
        public CapValuacionGlobalCliente Insertar(CapValuacionGlobalCliente datos, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValuacionGlobalClientes.Add(datos);
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el repositorio CapValuacionGlobalCliente
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapValuacionGlobalCliente]</returns>
        public IEnumerable<CapValuacionGlobalCliente> Consultar(int idEmp, int idCd, int idCte, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var valuaciones = from cvgc in ctx.CapValuacionGlobalClientes
                              where cvgc.Id_Emp==idEmp && cvgc.Id_Cd==idCd && cvgc.Id_Cte==idCte
                              select cvgc;
            return valuaciones;
        }

        /// <summary>
        /// Regresa el resultado de consultar los proyectos asociados a una valuación global
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idValGlobal">Identificador de la valuación global</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CrmOportunidade[]</returns>
        public IEnumerable<CrmOportunidade> ConsultarProyectosAsociados(int idEmp, int idCd, int idValGlobal, int idCte, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var proyectos = from vp in ctx.CrmValuacionOportunidades
                            where vp.Id_Emp==idEmp && vp.Id_Cd==idCd && vp.Id_Val==idValGlobal && vp.Id_Cte==idCte
                            select vp.CrmOportunidade;
            return proyectos;
        }
    }
}

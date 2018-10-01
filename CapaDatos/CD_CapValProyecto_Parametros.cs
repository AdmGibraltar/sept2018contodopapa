using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    /// <summary>
    /// Clase de acceso a datos al repositorio CapValProyecto_Parametros
    /// </summary>
    public class CD_CapValProyecto_Parametros
    {
        /// <summary>
        /// Inserta una nueva entrada en el repositorio CapValProyecto_Parametros.
        /// </summary>
        /// <param name="entityInstance">Instancia de datos de la entidad CapValProyecto_Parametros</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValProyecto_Parametros</returns>
        public CapValProyecto_Parametros Insertar(CapValProyecto_Parametros entityInstance, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValProyecto_Parametros.Add(entityInstance);
        }

        /// <summary>
        /// Deuelve el resultado de la consulta al repositorio CapValProyecto_Parametros dado el identificador de la valuación idVal.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValProyecto_Parametros</returns>
        public CapValProyecto_Parametros ConsultarPorValuacion(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var parametros = from cvpp in ctx.CapValProyecto_Parametros
                             where cvpp.Id_Emp==idEmp && cvpp.Id_Cd==idCd && cvpp.Id_Vap==idVal
                             select cvpp;
            if (parametros.Count() > 0)
            {
                return parametros.First();
            }
            return null;
        }
    }
}

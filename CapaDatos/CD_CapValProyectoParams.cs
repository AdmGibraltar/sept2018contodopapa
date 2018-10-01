using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    /// <summary>
    /// Clase de acceso a datos para el repositorio CapValProyectoParams
    /// </summary>
    public class CD_CapValProyectoParams
    {
        /// <summary>
        /// Inserta un registro en la tabla CapValProyectoParams.
        /// </summary>
        /// <param name="datos">Instancia de datos de la entidad CapValProyectoParam</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapValProyecto_Params</returns>
        public CapValProyecto_Params Insertar(CapValProyecto_Params datos, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapValProyecto_Params.Add(datos);
        }

        public CapValProyecto_Params Consultar(int idEmp, int idCd, int idVal, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var pars = from cvpp in ctx.CapValProyecto_Params
                       where cvpp.Id_Emp == idEmp && cvpp.Id_Cd == idCd && cvpp.Id_Vap == idVal
                       select cvpp;
            if (pars.Count() > 0)
                return pars.First();
            return null;
        }
    }
}

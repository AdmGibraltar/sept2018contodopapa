using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatRecursoArchivo
    {
        /// <summary>
        /// Inserta una entrada en el repositorio CatRecursoArchivo.
        /// </summary>
        /// <param name="catRecursoArchivo">Instancia de datos</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CatRecursoArchivo</returns>
        public CatRecursoArchivo Insertar(CatRecursoArchivo catRecursoArchivo, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CatRecursoArchivoes.Add(catRecursoArchivo);
        }

        /// <summary>
        /// Regresa el resultado de la consulta al repositorio CatRecursoArchivo condicionado por el identificador del recurso
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRecurso">Identificador del recurso</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns></returns>
        public CatRecursoArchivo ConsultarPorIdentificador(int idEmp, int idCd, int idRecurso, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from cra in ctx.CatRecursoArchivoes
                           where cra.Id_Emp==idEmp && cra.Id_Cd==idCd && cra.Id_Recurso==idRecurso
                           select cra;
            if (entradas.Count() > 0)
            {
                return entradas.First();
            }
            return null;
        }
    }
}

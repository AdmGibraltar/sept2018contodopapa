using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatTipoRecurso
    {
        /// <summary>
        /// Devuelve el resultado de la consulta al repositorio CatTipoRepositorio condicionado por el campo TipoRec_Nombre.
        /// </summary>
        /// <param name="nombre">Nombre del tipo de recurso</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CatTipoRecurso</returns>
        public CatTipoRecurso ConsultarPorIdNombre(string nombre, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from ctr in ctx.CatTipoRecursoes
                           where ctr.TipoRec_Nombre==nombre
                           select ctr;
            if (entradas.Count() > 0)
            {
                return entradas.First();
            }
            return null;
        }
    }
}

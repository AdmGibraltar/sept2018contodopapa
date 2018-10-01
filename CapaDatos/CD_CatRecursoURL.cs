using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatRecursoURL
    {
        /// <summary>
        /// Inserta una instancia de datos en el repositorio CatRecursoURL.
        /// </summary>
        /// <param name="instanciaDatos">Instancia de datos CatRecursoURL</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CatRecursoURL</returns>
        public CatRecursoURL Insertar(CatRecursoURL instanciaDatos, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CatRecursoURLs.Add(instanciaDatos);
        }
    }
}

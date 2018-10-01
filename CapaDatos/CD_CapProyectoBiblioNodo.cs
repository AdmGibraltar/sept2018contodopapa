using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapProyectoBiblioNodo
    {
        /// <summary>
        /// Inserta una nueva entrada en la entidad CapProyectoBiblioNodo
        /// </summary>
        /// <param name="entrada">Instancia de datos de la entidad CapProyectoBiblioNodo</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapProyectoBiblioNodo</returns>
        public CapProyectoBiblioNodo Insertar(CapProyectoBiblioNodo entrada, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapProyectoBiblioNodoes.Add(entrada);
        }
    }
}

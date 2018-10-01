using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapProyectoFasesBiblioNodo
    {
        /// <summary>
        /// Inserta una nueva entrada en la entidad CapProyectoFaseBiblioNodo
        /// </summary>
        /// <param name="entrada">Instancia de datos de la entidad CapProyectoFaseBiblioNodo</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapProyectoFaseBiblioNodo</returns>
        public CapProyectoFaseBiblioNodo Insertar(CapProyectoFaseBiblioNodo entrada, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapProyectoFaseBiblioNodoes.Add(entrada);
        }
    }
}

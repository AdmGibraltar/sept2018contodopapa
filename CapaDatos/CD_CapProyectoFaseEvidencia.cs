using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapProyectoFaseEvidencia
    {
        /// <summary>
        /// Regresa la consulta sobre la entidad CapProyectoFaseEvidencia.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idU">Identificador del usuario</param>
        /// <param name="idBiblio">Identificador de la biblioteca</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <param name="idFase">Identificador de la fase</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos.</param>
        /// <returns>IQueryable<CapProyectoFaseEvidencia></returns>
        public IQueryable<CapProyectoFaseEvidencia> ConsultarPorProyecto(int idEmp, int idCd, int idU, int idBiblio, int idOp, int idFase, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var evidencias = from e in ctx.CapProyectoFaseEvidencias
                             where e.Id_Emp==idEmp && e.Id_Cd==idCd && e.Id_U==idU && e.Id_Biblioteca==idBiblio && e.Id_Op==idOp && e.Id_Fase==idFase
                             select e;
            return evidencias;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmControlEntrada
    {
        /// <summary>
        /// Consulta básica al repositorio CrmControlEntrada.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CRMControlEntrada]</returns>
        public IEnumerable<CRMControlEntrada> Consultar(int idEmp, int idCd, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from e in ctx.CRMControlEntradas
                           where e.Id_Emp==idEmp && e.Id_Cd==idCd
                           select e;
            return entradas;
        }
    }
}

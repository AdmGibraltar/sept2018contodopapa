using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo.SIANCentral;

namespace CapaDatos.SIANCentral
{
    public class CD_CatCDI
    {
        /// <summary>
        /// Regresa el resultado de la consulta sobre el repositorio CatCDI.
        /// </summary>
        /// <param name="icdCtx"></param>
        /// <returns></returns>
        public IEnumerable<CatCDI> Consultar(ICD_Contexto icdCtx)
        {
            SIANCentralEntities1 ctx = ((ICD_Contexto<SIANCentralEntities1>)icdCtx).Contexto;
            return from cdi in ctx.CatCDIs
                   select cdi;
        }
    }
}

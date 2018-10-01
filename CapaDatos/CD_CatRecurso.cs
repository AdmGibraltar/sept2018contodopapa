using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatRecurso
    {
        public CatRecurso Insertar(CatRecurso catRecurso, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CatRecursoes.Add(catRecurso);
        }
    }
}

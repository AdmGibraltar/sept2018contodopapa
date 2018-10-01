using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmCausasCancelacion
    {
        public CD_CrmCausasCancelacion(ICD_Contexto icdCtx)
        {
            m_icdCtx = icdCtx;
        }

        public IEnumerable<crmCausasCancelacion> Obtener()
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)m_icdCtx).Contexto;
            return ctx.crmCausasCancelacions;
        }

        /// <summary>
        /// Inserta una nueva causa en el repositorio crmCausasCancelacion
        /// </summary>
        /// <param name="causa">Instancia de la entidad crmCausasCancelacion</param>
        /// <returns>Instancia de la entidad crmCausaCancelacion insertada</returns>
        public crmCausasCancelacion Insertar(crmCausasCancelacion causa)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)m_icdCtx).Contexto;
            return ctx.crmCausasCancelacions.Add(causa);
        }

        protected ICD_Contexto m_icdCtx = null;
    }
}

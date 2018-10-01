using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmCausasCancelacion
    {
        public CN_CrmCausasCancelacion(IBusinessTransaction ibt, CD_CrmCausasCancelacion cdCrmCausasCancelacion)
        {
            m_ibt = ibt;
            m_cdCrmCausasCancelacion = cdCrmCausasCancelacion;
        }

        public static CN_CrmCausasCancelacion Crear(IBusinessTransaction ibt)
        {
            return new CN_CrmCausasCancelacion(ibt, new CD_CrmCausasCancelacion(ibt.DataContext));
        }

        public IEnumerable<crmCausasCancelacion> ObtenerCausasFinales()
        {
            var causas = m_cdCrmCausasCancelacion.Obtener().Where(c => c.SubCausas.Count() == 0);
            return causas;
        }
        
        protected IBusinessTransaction m_ibt = null;
        protected CD_CrmCausasCancelacion m_cdCrmCausasCancelacion=null;
    }
}

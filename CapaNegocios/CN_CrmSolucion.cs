using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmSolucion
    {
        public void ComboUen(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboUen(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

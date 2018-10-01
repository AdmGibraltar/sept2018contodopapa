using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatClienteDetGarantia
    {
        public CN_CatClienteDetGarantia(Sesion sesion)
        {
            _sesion = sesion;
        }

#warning "Necesita parámetros de Empresa, Centro de Distribución"
        public List<CatClienteDetGarantia> ObtenerTodos(int? Id_Cte, int? Id_CteDet)
        {
            CD_CatClienteDetGarantia cdCg = new CD_CatClienteDetGarantia(_sesion.Emp_Cnx_EF);
            return cdCg.ObtenerTodos(_sesion.Id_Emp, _sesion.Id_Cd, Id_Cte, Id_CteDet);
        }

        public List<CatClienteDetGarantia> ObtenerTodos_NOEF(int? Id_Cte, int? Id_CteDet)
        {
            CD_CatClienteDetGarantia cdCg = new CD_CatClienteDetGarantia(_sesion.Emp_Cnx);
            return cdCg.ObtenerTodos_NOEF(_sesion.Id_Emp, _sesion.Id_Cd, Id_Cte, Id_CteDet);
        }

        private Sesion _sesion;
    }
}

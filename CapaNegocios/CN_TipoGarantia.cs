using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_TipoGarantia
    {
        public CN_TipoGarantia(Sesion sesion)
        {
            _sesion = sesion;
        }

        public List<CatTipoGarantia> xObtenerTodas()
        {
            CD_TipoGarantia cdTg = new CD_TipoGarantia(_sesion.Emp_Cnx_EF);
            return cdTg.ObtenerTodos();
        }

        public List<CatTipoGarantia> ObtenerTodas()
        {
            CD_TipoGarantia cdTg = new CD_TipoGarantia(_sesion.Emp_Cnx);
            return cdTg.ObtenerTodos_NOEF();
        }

        public CatTipoGarantia Consultar(int idTg)
        {
            CD_TipoGarantia cdTg = new CD_TipoGarantia(_sesion.Emp_Cnx_EF);
            return cdTg.Consultar(idTg);
        }

        private Sesion _sesion;
    }
}

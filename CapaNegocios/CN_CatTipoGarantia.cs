using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatTipoGarantia
    {
        public CN_CatTipoGarantia(Sesion sesion)
        {
            _sesion = sesion;
        }

        public CatTipoGarantia Tradicional
        {
            get
            {
                if (_tradicional == null)
                {
                    CD_CatTipoGarantia cdTg = new CD_CatTipoGarantia(_sesion.Emp_Cnx_EF);
                    _tradicional = cdTg.ConsultarPorNombre("REGULAR");
                }
                return _tradicional;
            }
        }

        private CatTipoGarantia _tradicional=null;
        private Sesion _sesion = null;
    }
}

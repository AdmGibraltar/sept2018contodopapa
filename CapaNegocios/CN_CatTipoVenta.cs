using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatTipoVenta
    {
        public CN_CatTipoVenta(Sesion sesion)
        {
            _sesion = sesion;
        }

        public CatTipoVenta Tradicional
        {
            get
            {
                if (_Tradicional == null)
                {
                    CD_CatTipoVenta cdTv = new CD_CatTipoVenta(_sesion.Emp_Cnx_EF);
                    _Tradicional = cdTv.ConsultarPorNombre("REGULAR");
                }

                return _Tradicional;
            }
        }

        private CatTipoVenta _Tradicional = null;

        private Sesion _sesion = null;
    }
}

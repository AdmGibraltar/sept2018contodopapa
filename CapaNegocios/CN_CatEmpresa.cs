using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatEmpresa
    {
        public IEnumerable<CatUEN> ObtenerUENs(Sesion sesion)
        {
            CD_CatUen cdCatUen = new CD_CatUen();
            var resultado = cdCatUen.ConsultarPorEmpresa(sesion.Id_Emp, sesion.Emp_Cnx_EF);
            return resultado;
        }
    }
}

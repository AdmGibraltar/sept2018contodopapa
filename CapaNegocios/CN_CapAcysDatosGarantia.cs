using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapAcysDatosGarantia
    {
        public CapAcysDatosGarantia Consultar(int? id_Emp, int? id_Cd, int? id_Ter, int? id_Cte, int? id_TG, Sesion s)
        {
            CD_CapAcysDatosGarantia cdCapAcysDatosGarantia = new CD_CapAcysDatosGarantia();
            var res = cdCapAcysDatosGarantia.Consultar(id_Emp, id_Cd, id_Ter, id_Cte, id_TG, s.Emp_Cnx_EF);
            return res;
        }
    }
}

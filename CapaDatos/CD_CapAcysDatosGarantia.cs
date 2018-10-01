using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
namespace CapaDatos
{
    public class CD_CapAcysDatosGarantia
    {
        public CapAcysDatosGarantia Consultar(int? id_Emp, int? id_Cd, int? id_Ter, int? id_Cte, int? id_TG, string cadenaDeConexionEF)
        {
            CapAcysDatosGarantia ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var res = ctx.spCapAcysDatosGarantia_Consultar(id_Emp, id_Cd, id_Ter, id_Cte, id_TG).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }
    }
}

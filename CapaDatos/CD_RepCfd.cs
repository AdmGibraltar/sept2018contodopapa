using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_RepCfd
    {
        public void Consultar(CapaEntidad.Sesion session, CapaEntidad.Cfd cfd, ref System.Collections.ArrayList verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(session.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@FechaIni", "@Fechafin", "@OrdenActivos" };
                object[] Valores = { session.Id_Emp, session.Id_Cd_Ver, Convert.ToDateTime("01/" + cfd.FiltroMes + "/" + cfd.FiltroAnhio), Convert.ToDateTime("01/" + cfd.FiltroMes + "/" + cfd.FiltroAnhio).AddMonths(1).AddDays(-1), cfd.OrdenActivos };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_CFD", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    verificador.Add(dr.GetValue(dr.GetOrdinal("Cadena")).ToString());
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

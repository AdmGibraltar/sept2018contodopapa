using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Contraseña
    {
        public void ConsultaLongitudPass(ConfiguracionGlobal ConfiguracionGlobal, string Conexion, ref System.Collections.Generic.List<ConfiguracionGlobal> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Conf" };
                object[] Valores = { ConfiguracionGlobal.Id_Cd, 13 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpSysConfiguracion_ConsultaLongitud", ref dr, Parametros, Valores);

                ConfiguracionGlobal VarConfiguracion = default(ConfiguracionGlobal);
                while (dr.Read())
                {
                    VarConfiguracion = new ConfiguracionGlobal();
                    VarConfiguracion.Contraseña_Long_Min = (string)dr.GetValue(dr.GetOrdinal("Conf_Valor"));
                    list.Add(VarConfiguracion);
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

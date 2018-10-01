using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapDevolucion
    {
        public void ConsultarCantidadDevolucionesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapDevolucionesCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

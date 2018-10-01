using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ProCancelaMenores
    {
        public void Cancelar(CentroDistribucion cd, DateTime dateTime1, DateTime dateTime2, string Conexion, ref int verificadorFact, ref int verificadorNcar)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@FechaIni", "@FechaFin" };
                object[] Valores = { cd.Id_Emp, cd.Id_Cd, dateTime1, dateTime2 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCancelacionSaldoMenor", ref dr, Parametros, Valores);

                dr.Read();
                verificadorFact = (int)dr[0];
                verificadorNcar = (int)dr[1];

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
   public class CD_CatZonas
    {
        public void Consultar(int Id_Emp, int Id_Cd, ref List<CapaEntidad.CentroDistribucion> list, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CatZonas_Lista", ref dr, Parametros, Valores);
                CentroDistribucion cd;

                while (dr.Read())
                {
                    cd = new CentroDistribucion();
                    cd.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cd.Cd_Descripcion = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    cd.Generico = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Checked")));

                    list.Add(cd);
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

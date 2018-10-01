using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
  public  class CD_Pagina
    {

      public void PaginaConsultar(ref CapaEntidad.Pagina pagina, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Url" };
                object[] Valores = { pagina.Url };
                 
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysPagina", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pagina.Clave = (int)dr.GetValue(dr.GetOrdinal("Sm_Cve"));
                    pagina.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Sm_Desc"));
                    pagina.Path = (string)dr.GetValue(dr.GetOrdinal("Sm_Path"));
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

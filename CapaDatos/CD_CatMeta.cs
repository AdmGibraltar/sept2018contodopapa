using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatMeta
    {
        public void Lista(Meta meta, string Conexion, ref List<Meta> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U" };
                object[] Valores = { meta.Id_Emp, meta.Id_Cd, null };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepresentantesSucursalMetas", ref dr, Parametros, Valores);//spCapMetas_Lista

                while (dr.Read())
                {
                    meta = new Meta();
                    meta.Id_Met = (int)dr.GetValue(dr.GetOrdinal("Id_Met"));
                    meta.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    meta.Cd_Nombre = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                    meta.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    meta.Rik_Nombre = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    meta.Met_Proyectos = (int)dr.GetValue(dr.GetOrdinal("Met_Proyectos"));
                    meta.Met_MontoProyecto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Met_MontoProyecto")));
                    meta.Met_Avances = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_Avances")));
                    meta.Met_CantCerrado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_CantCerrado")));
                    meta.Met_MontCerrado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Met_MontCerrado")));
                    List.Add(meta);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Meta meta, Sesion sesion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_U",
                                          "@Id_Metas", 
                                          "@Cantidad", 
                                          "@Monto", 
                                          "@Met_Avances", 
                                          "@Met_CantCerrado",
                                          "@Met_MontCerrado"
                                      };
                object[] Valores = { 
                                       meta.Id_Emp,
                                       meta.Id_Cd,
                                       meta.Id_Rik,
                                       meta.Id_Met,
                                       meta.Met_Proyectos,
                                       meta.Monto,
                                       meta.Met_Avances,
                                       meta.Met_CantCerrado,
                                       meta.Met_MontCerrado                                    
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepresentantesSucursalMetas_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(int Id_U, ref Meta meta, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_U"
                                      };
                object[] Valores = { 
                                       meta.Id_Emp,                                        
                                       meta.Id_Cd,
                                       Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMMeta_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    meta.Met_Proyectos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_Proyectos")));
                    meta.Met_MontoProyecto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_MontoProyecto")));
                    meta.Met_Avances = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_Avances")));
                    meta.Met_CantCerrado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_CantCerrado")));
                    meta.Met_MontCerrado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Met_MontCerrado")));
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

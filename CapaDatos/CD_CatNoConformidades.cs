using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatNoConformidades
    {
        public void ConsultaNoConformidades(NoConformidades NoConformidades, string Conexion, ref List<NoConformidades> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                      };
                object[] Valores = { 
                                        NoConformidades.Id_Emp,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNoConformidades_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    NoConformidades = new NoConformidades();
                    NoConformidades.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    NoConformidades.Id_Nco = (int)dr.GetValue(dr.GetOrdinal("Id_Nco"));
                    NoConformidades.Nco_Tipo = (int)dr.GetValue(dr.GetOrdinal("Nco_Tipo"));
                    NoConformidades.Nco_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Nco_Descripcion"));
                    NoConformidades.Nco_Aplica = (string)dr.GetValue(dr.GetOrdinal("Nco_Aplica"));
                    NoConformidades.Nco_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Nco_Activo")));
                    if (Convert.ToBoolean(NoConformidades.Nco_Activo))
                    {
                        NoConformidades.Nco_ActivoStr = "Activo";
                    }
                    else
                    {
                        NoConformidades.Nco_ActivoStr = "Inactivo";
                    }
                    List.Add(NoConformidades);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarNoConformidades(NoConformidades NoConformidades, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Nco",
                                        "@Nco_Descripcion",
                                        "@Nco_Aplica",
                                        "@Nco_Tipo",
                                        "@Nco_Activo"
                                      };
                object[] Valores = { 
                                        NoConformidades.Id_Emp,
                                        NoConformidades.Id_Nco,
                                        NoConformidades.Nco_Descripcion,
                                        NoConformidades.Nco_Aplica,
                                        NoConformidades.Nco_Tipo,
                                        NoConformidades.Nco_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNoConformidades_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarNoConformidades(NoConformidades NoConformidades, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Nco",
                                        "@Id_Nco_Ant",
                                        "@Nco_Descripcion",
                                        "@Nco_Aplica",
                                        "@Nco_Tipo",
                                        "@Nco_Activo",
                                        "@Nco_TipoAnt"
                                      };
                object[] Valores = { 
                                        NoConformidades.Id_Emp,
                                        NoConformidades.Id_Nco,
                                        NoConformidades.Id_Nco_Ant,
                                        NoConformidades.Nco_Descripcion,
                                        NoConformidades.Nco_Aplica,
                                        NoConformidades.Nco_Tipo,
                                        NoConformidades.Nco_Activo,
                                        NoConformidades.Nco_TipoAnt
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNoConformidades_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

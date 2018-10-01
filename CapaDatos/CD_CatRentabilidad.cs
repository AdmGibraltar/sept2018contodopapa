using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatRentabilidad
    {
        public void ConsultaRentabilidad(int Id_Emp, string Conexion, ref List<Rentabilidad> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRentabilidad_Consulta", ref dr, Parametros, Valores);

                Rentabilidad rentabilidad;
                while (dr.Read())
                {
                    rentabilidad = new Rentabilidad();
                    rentabilidad.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    rentabilidad.Id_Ren = (int)dr.GetValue(dr.GetOrdinal("Id_Ren"));
                    rentabilidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Ren_Descripcion"));
                    rentabilidad.Nivel = (string)dr.GetValue(dr.GetOrdinal("Ren_Nivel"));
                    rentabilidad.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ren_Activo")));
                    if (Convert.ToBoolean(rentabilidad.Estatus))
                    {
                        rentabilidad.EstatusStr = "Activo";
                    }
                    else
                    {
                        rentabilidad.EstatusStr = "Inactivo";
                    }
                    List.Add(rentabilidad);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRentabilidad(Rentabilidad rentabilidad, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Ren",
	                                    "@Ren_Nivel", 
	                                    "@Ren_Descripcion", 
                                        "@Ren_Activo"
                                      };
                object[] Valores = { 
                                        rentabilidad.Id_Emp,
                                        rentabilidad.Id_Ren,
                                        rentabilidad.Nivel,
                                        rentabilidad.Descripcion,
                                        rentabilidad.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRentabilidad_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRentabilidad(Rentabilidad rentabilidad, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Ren",
	                                    "@Ren_Nivel", 
	                                    "@Ren_Descripcion", 
                                        "@Ren_Activo"
                                      };
                object[] Valores = { 
                                        rentabilidad.Id_Emp,
                                        rentabilidad.Id_Ren,
                                        rentabilidad.Nivel,
                                        rentabilidad.Descripcion,
                                        rentabilidad.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRentabilidad_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

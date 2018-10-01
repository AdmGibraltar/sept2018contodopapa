using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatRutas
    {
        public void ConsultaRuta(Ruta Ruta, string Conexion, ref List<Ruta> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd"
                                      };
                object[] Valores = { 
                                        Ruta.Id_Emp,
                                        Ruta.Id_Cd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutas_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Ruta = new Ruta();
                    Ruta.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    Ruta.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Ruta.Id_Rut = (int)dr.GetValue(dr.GetOrdinal("Id_Rut"));
                    Ruta.Rut_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Rut_Descripcion"));
                    Ruta.Rut_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Rut_Activo")));
                    if (Convert.ToBoolean(Ruta.Rut_Activo))
                    {
                        Ruta.Rut_ActivoStr = "Activo";
                    }
                    else
                    {
                        Ruta.Rut_ActivoStr = "Inactivo";
                    }
                    List.Add(Ruta);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarRuta(Ruta Ruta, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Rut",
                                        "@Rut_Descripcion", 
                                        "@Rut_Activo"
                                      };
                object[] Valores = { 
                                        Ruta.Id_Emp, 
                                        Ruta.Id_Cd,
                                        Ruta.Id_Rut,
                                        Ruta.Rut_Descripcion, 
                                        Ruta.Rut_Activo 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutas_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarRuta(Ruta Ruta, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Rut",
                                        "@Id_Rut_Ant",
                                        "@Rut_Descripcion", 
                                        "@Rut_Activo"
                                      };
                object[] Valores = { 
                                        Ruta.Id_Emp, 
                                        Ruta.Id_Cd,
                                        Ruta.Id_Rut,
                                        Ruta.Id_Rut_Ant,
                                        Ruta.Rut_Descripcion, 
                                        Ruta.Rut_Activo 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutas_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

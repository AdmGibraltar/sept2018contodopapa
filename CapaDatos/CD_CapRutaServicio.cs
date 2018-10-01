using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapRutaServicio
    {
        public void ConsultaCapRutaServicio(int Id_Emp, int Id_Cd, string Conexion, ref List<RutaServicio> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRutaServicio_Consulta", ref dr, Parametros, Valores);
                                
                RutaServicio rutaServicio;
                while (dr.Read())
                {
                    rutaServicio = new RutaServicio();
                    rutaServicio.Id_Cap = (int)dr.GetValue(dr.GetOrdinal("Id_Crs"));
                    rutaServicio.Id_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    rutaServicio.Aparatos = (int)dr.GetValue(dr.GetOrdinal("Crs_Revisados"));
                    rutaServicio.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Crs_Fecha"));
                    rutaServicio.Id_Ruta = (int)dr.GetValue(dr.GetOrdinal("Id_Rut"));
                    rutaServicio.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    rutaServicio.Rut_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Rut_Descripcion"));  
                    rutaServicio.Num_Semana = (int) dr.GetValue(dr.GetOrdinal("Num_Semana"));
                    rutaServicio.Fecha_InicioSemana = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_InicioSemana"));
                    rutaServicio.Fecha_FinSemana = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_FinSemana"));
                    List.Add(rutaServicio);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCapRutaServicio(int Id_Emp, int Id_Cd, int Id_U, RutaServicio rutaServicio, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                       "@Id_Emp",          
                                       "@Id_Cd",      
                                       "@Id_Crs",    
                                       "@Id_U",    
                                       "@Id_Cliente",           
                                       "@Aparatos",
                                       "@Fecha",          
                                       "@Id_Ruta"                                                                                     
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd,
                                       rutaServicio.Id_Cap,
                                       Id_U,
                                       rutaServicio.Id_Cliente,
                                       rutaServicio.Aparatos,
                                       rutaServicio.Fecha,
                                       rutaServicio.Id_Ruta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRutaServicio_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCapRutaServicio(int Id_Emp, int Id_Cd, int Id_U, RutaServicio rutaServicio, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Crs",      
                                         "@Id_Emp",        
                                         "@Id_Cd",        
                                         "@Id_U",    
                                         "@Id_Cliente",         
                                         "@Aparatos",        
                                         "@Fecha",        
                                         "@Id_Ruta"                                                                    
                                      };
                object[] Valores = { 
                                       rutaServicio.Id_Cap,
                                       Id_Emp,
                                       Id_Cd,
                                       Id_U,
                                       rutaServicio.Id_Cliente,
                                       rutaServicio.Aparatos,
                                       rutaServicio.Fecha,
                                       rutaServicio.Id_Ruta
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRutaServicio_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCapRutaServicio(int Id_Emp, int Id_Cd,RutaServicio rutaServicio, string Conexion, ref int verificador)
        {
           
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Cap",      
                                         "@Id_Cliente",        
                                         "@Id_Ruta" ,
                                         "@Id_Empresa",
	                                     "@Id_CD "                                                                                                           
                                      };
                object[] Valores = { 
                                       rutaServicio.Id_Cap,                                      
                                       rutaServicio.Id_Cliente,                                       
                                       rutaServicio.Id_Ruta,
                                       Id_Emp,
                                       Id_Cd
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRutaServicio_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

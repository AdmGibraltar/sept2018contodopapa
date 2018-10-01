using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CrmCatSoluciones
    {
        public void ComboUen(Sesion sesion, ref List<CrmCatSolucion> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { 1, sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCRMUen_Combo", ref dr, Parametros, Valores);

                CrmCatSolucion catSoluciones;
                while (dr.Read())
                {
                    catSoluciones = new CrmCatSolucion();
                    catSoluciones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catSoluciones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catSoluciones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int uen, ref List<CrmCatSolucion> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, uen };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCRMSegmentosUen_Combo", ref dr, Parametros, Valores);

                CrmCatSolucion catSoluciones;
                while (dr.Read())
                {
                    catSoluciones = new CrmCatSolucion();
                    catSoluciones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catSoluciones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catSoluciones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmCatSolucion> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, segmento };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCRMAreaSegmento_Combo", ref dr, Parametros, Valores);

                CrmCatSolucion catSoluciones;
                while (dr.Read())
                {
                    catSoluciones = new CrmCatSolucion();
                    catSoluciones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catSoluciones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catSoluciones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatSolucion(Sesion sesion, int area, ref List<CrmCatSolucion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Area" };
                object[] Valores = { sesion.Id_Emp, area };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones", ref dr, Parametros, Valores);

                CrmCatSolucion catSolucion;
                while (dr.Read())
                {
                    catSolucion = new CrmCatSolucion();
                    catSolucion.Clave = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    catSolucion.Solucion = (string)dr.GetValue(dr.GetOrdinal("Sol_Descripcion"));
                    catSolucion.Porcentaje = (double)dr.GetValue(dr.GetOrdinal("Sol_Potencial"));
                    List.Add(catSolucion);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertCatSolucion(Sesion sesion, CrmCatSolucion solucion, ref int valido)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                       "@Id_Emp",          
                                       "@Id_Sol",      
                                       "@Sol_Descripcion",    
                                       "@Id_Area",    
                                       "@Sol_Potencial"                                                                              
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       solucion.Id,
                                       solucion.Descripcion,
                                       solucion.Area,
                                       solucion.Porcentaje
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Insertar", ref valido, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCatSolucion(Sesion sesion, CrmCatSolucion solucion, ref int valido)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { 
                                       "@Id_Emp",          
                                       "@Id_Sol",      
                                       "@Sol_Descripcion",    
                                       "@Id_Area",    
                                       "@Sol_Potencial"                                                                              
                                      };
                object[] Valores = { 
                                       sesion.Id_Emp,
                                       solucion.Clave,
                                       solucion.Descripcion,
                                       solucion.Area,
                                       solucion.Porcentaje
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Eliminar", ref valido, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSolucion(CrmCatSolucion soluciones, string Conexion, int Id_Emp, ref int valido)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                       "@Id_Emp",          
                                       "@Id_Sol",      
                                       "@Sol_Descripcion",    
                                       "@Sol_Potencial"                                                                              
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       soluciones.Clave,
                                       soluciones.Descripcion,
                                       soluciones.Porcentaje
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Modificar", ref valido, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

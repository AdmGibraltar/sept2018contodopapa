using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatRutaEntrega
    {
        public void ConsultaRutaEntrega(RutaEntrega ruta, string Conexion, ref List<RutaEntrega> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { ruta.Id_Emp, ruta.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutaEntrega_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    ruta = new RutaEntrega();
                    ruta.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    ruta.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    ruta.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Rut"));
                    ruta.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Rut_Descripcion"));
                    ruta.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    ruta.Incidencia = (int)dr.GetValue(dr.GetOrdinal("Rut_Incidencia"));
                    ruta.Sem_Ini = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rut_SemIni"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Rut_SemIni"));
                    ruta.Dia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rut_Dia"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Rut_Dia"));
                    ruta.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Rut_Activo")));
                    if (Convert.ToBoolean(ruta.Estatus))
                    {
                        ruta.EstatusStr = "Activa";
                    }
                    else
                    {
                        ruta.EstatusStr = "Inactiva";
                    }
                    List.Add(ruta);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRutaEntrega(RutaEntrega ruta, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
		                                "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Rut", 
		                                "@Id_Ter", 
		                                "@Rut_Descripcion", 
		                                "@Rut_SemIni", 
		                                "@Rut_Incidencia", 
		                                "@Rut_Dia", 
		                                "@Rut_Activo"
                                      };
                object[] Valores = { 
                                        ruta.Id_Emp,
                                        ruta.Id_Cd,
                                        ruta.Id,
                                        ruta.Id_Ter,
                                        ruta.Descripcion,
                                        ruta.Sem_Ini == -1 ? (object)null : ruta.Sem_Ini,
                                        ruta.Incidencia,
                                        ruta.Dia == -1 ? (object)null : ruta.Dia,
                                        ruta.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutaEntrega_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRutaEntrega(RutaEntrega ruta, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
		                                "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Rut", 
                                        "@Id_Rut_Ant", 
		                                "@Id_Ter", 
		                                "@Rut_Descripcion", 
		                                "@Rut_SemIni", 
		                                "@Rut_Incidencia", 
		                                "@Rut_Dia", 
		                                "@Rut_Activo"
                                      };
                object[] Valores = { 
                                         ruta.Id_Emp,
                                        ruta.Id_Cd,
                                        ruta.Id,
                                        ruta.Id_Ant,
                                        ruta.Id_Ter,
                                        ruta.Descripcion,
                                        ruta.Sem_Ini == -1 ? (object)null : ruta.Sem_Ini,
                                        ruta.Incidencia,
                                        ruta.Dia == -1 ? (object)null : ruta.Dia,
                                        ruta.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRutaEntrega_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

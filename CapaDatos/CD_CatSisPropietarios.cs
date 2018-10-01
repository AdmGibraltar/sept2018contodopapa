using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatSisPropietarios
    {
        public void ConsultaSisPropietarios(int  Id_Emp, string Conexion, ref List<SistemasPropietarios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSisPropietarios_Consulta", ref dr, Parametros, Valores);

                SistemasPropietarios sp;
                while (dr.Read())
                {
                    sp = new SistemasPropietarios();
                    sp.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Spo"));
                    sp.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Spo_Descripcion"));
                    sp.Factor = (double)dr.GetValue(dr.GetOrdinal("Spo_Factor"));
                    sp.Clase = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Spo_Clase")));
                    sp.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Spo_Activo")));
                    if (Convert.ToBoolean(sp.Estatus))
                    {
                        sp.EstatusStr = "Activo";
                    }
                    else
                    {
                        sp.EstatusStr = "Inactivo";
                    }
                    List.Add(sp);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSisPropietarios(SistemasPropietarios sp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_spo",
                                        "@Id_Emp",
	                                    "@Spo_Descripcion", 
	                                    "@Spo_Clase", 
	                                    "@Spo_Factor", 
	                                    "@Spo_Activo"
                                      };
                object[] Valores = { 
                                        sp.Id,
                                        sp.Id_Emp,
                                        sp.Descripcion,
                                        sp.Clase,
                                        sp.Factor,
                                        sp.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSisPropietarios_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSisPropietarios(SistemasPropietarios sp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                       "@Id_spo", 
                                       "@Id_spo_Ant",
                                       "@Id_Emp",
	                                   "@Spo_Descripcion", 
	                                   "@Spo_Clase", 
	                                   "@Spo_Factor", 
	                                   "@Spo_Activo"
                                      };
                object[] Valores = { 
                                        sp.Id,
                                        sp.Id_Anterior,
                                        sp.Id_Emp,
                                        sp.Descripcion,
                                        sp.Clase,
                                        sp.Factor,
                                        sp.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSisPropietarios_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
